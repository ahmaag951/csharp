// ============================================================
// LDAP Login UI — ASP.NET Core minimal API
// ============================================================
// Runs a local web server with a simple login form.
// Enter any username from ldap.forumsys.com (e.g. "einstein")
// and password "password" to authenticate via LDAP bind.
// ============================================================

using Novell.Directory.Ldap;

const string LdapHost = "ldap.forumsys.com";
const int    LdapPort = 389;
const string BaseDn   = "dc=example,dc=com";

var builder = WebApplication.CreateBuilder(args);
var app     = builder.Build();

// GET / — render the login form
app.MapGet("/", () => Results.Content(LoginPage(message: null, success: null), "text/html"));

// POST /login — attempt LDAP bind with the supplied credentials
app.MapPost("/login", async (HttpRequest request) =>
{
    var form     = await request.ReadFormAsync();
    var username = form["username"].ToString().Trim();
    var password = form["password"].ToString();

    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        return Results.Content(LoginPage("Please enter both username and password.", success: false), "text/html");

    // Build the user's DN from the username
    // ldap.forumsys.com stores users as: uid=<username>,dc=example,dc=com
    var userDn = $"uid={username},{BaseDn}";

    try
    {
        using var conn = new LdapConnection();
        await conn.ConnectAsync(LdapHost, LdapPort);

        // Attempt bind — this is how LDAP verifies credentials
        await conn.BindAsync(userDn, password);

        // If BindAsync doesn't throw, credentials are valid
        // Fetch the user's display name to show in the success message
        string[] attrs   = ["cn", "mail"];
        var      results = await conn.SearchAsync(BaseDn, LdapConnection.ScopeBase,
                                                  "(objectClass=*)", attrs, typesOnly: false,
                                                  // search the user's own entry
                                                  new LdapSearchConstraints { MaxResults = 1 });

        // Re-search by uid to get display name since ScopeBase on userDn is cleaner
        var userResults = await conn.SearchAsync(BaseDn, LdapConnection.ScopeSub,
                                                 $"(uid={username})", attrs, typesOnly: false);

        string displayName = username;
        await foreach (var entry in userResults)
        {
            displayName = entry.GetStringValueOrDefault("cn", username)!;
            break;
        }

        return Results.Content(
            LoginPage($"Welcome, {displayName}! LDAP authentication succeeded.", success: true),
            "text/html");
    }
    catch (LdapException ex) when (ex.ResultCode == 49) // 49 = Invalid Credentials
    {
        return Results.Content(
            LoginPage("Invalid username or password.", success: false),
            "text/html");
    }
    catch (LdapException ex)
    {
        return Results.Content(
            LoginPage($"LDAP error ({ex.ResultCode}): {ex.Message}", success: false),
            "text/html");
    }
});

Console.WriteLine("LDAP Login UI is running at http://localhost:5050");
Console.WriteLine("Test users: einstein, newton, tesla, galileo, euler, gauss, curie ...");
Console.WriteLine("Password for all test users: password");
app.Run("http://localhost:5050");

// ── HTML helper ──────────────────────────────────────────────
static string LoginPage(string? message, bool? success) => $$"""
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>LDAP Login Demo</title>
  <style>
    * { box-sizing: border-box; margin: 0; padding: 0; }
    body {
      min-height: 100vh;
      display: flex;
      align-items: center;
      justify-content: center;
      background: #f0f4f8;
      font-family: system-ui, sans-serif;
    }
    .card {
      background: white;
      border-radius: 12px;
      box-shadow: 0 4px 24px rgba(0,0,0,.1);
      padding: 2.5rem 2rem;
      width: 100%;
      max-width: 380px;
    }
    h1 { font-size: 1.4rem; margin-bottom: .25rem; color: #1a202c; }
    .subtitle { font-size: .85rem; color: #718096; margin-bottom: 1.75rem; }
    label { display: block; font-size: .85rem; font-weight: 600;
            color: #4a5568; margin-bottom: .35rem; }
    input[type=text], input[type=password] {
      width: 100%; padding: .6rem .8rem; border: 1px solid #cbd5e0;
      border-radius: 6px; font-size: .95rem; margin-bottom: 1rem;
      transition: border-color .15s;
    }
    input:focus { outline: none; border-color: #4299e1;
                  box-shadow: 0 0 0 3px rgba(66,153,225,.25); }
    button {
      width: 100%; padding: .7rem; background: #4299e1; color: white;
      border: none; border-radius: 6px; font-size: 1rem; font-weight: 600;
      cursor: pointer; transition: background .15s;
    }
    button:hover { background: #3182ce; }
    .alert {
      padding: .75rem 1rem; border-radius: 6px; font-size: .9rem;
      margin-bottom: 1.25rem;
    }
    .alert.ok  { background: #c6f6d5; color: #276749; border: 1px solid #9ae6b4; }
    .alert.err { background: #fed7d7; color: #9b2c2c; border: 1px solid #fc8181; }
    .hint {
      margin-top: 1.25rem; font-size: .78rem; color: #a0aec0;
      border-top: 1px solid #e2e8f0; padding-top: 1rem;
    }
    .hint strong { color: #718096; }
  </style>
</head>
<body>
  <div class="card">
    <h1>LDAP Login Demo</h1>
    <p class="subtitle">Authenticates against <strong>ldap.forumsys.com</strong></p>

    {{(message is not null
        ? $"""<div class="alert {(success == true ? "ok" : "err")}">{System.Web.HttpUtility.HtmlEncode(message)}</div>"""
        : "")}}

    <form method="post" action="/login">
      <label for="username">Username</label>
      <input id="username" name="username" type="text"
             placeholder="e.g. einstein" autocomplete="username" required />

      <label for="password">Password</label>
      <input id="password" name="password" type="password"
             placeholder="password" autocomplete="current-password" required />

      <button type="submit">Sign in via LDAP</button>
    </form>

    <div class="hint">
      <strong>Test users (password: <code>password</code>):</strong><br/>
      einstein &nbsp;·&nbsp; newton &nbsp;·&nbsp; tesla &nbsp;·&nbsp; galileo
      &nbsp;·&nbsp; euler &nbsp;·&nbsp; gauss &nbsp;·&nbsp; curie
      &nbsp;·&nbsp; nobel &nbsp;·&nbsp; boyle &nbsp;·&nbsp; pasteur
    </div>
  </div>
</body>
</html>
""";
