// ============================================================
// SAMPLE: LDAP — Connect, Bind, Search & Read Attributes
// ============================================================
//
// Uses the public read-only LDAP test server at ldap.forumsys.com
// No setup required — just run the project.
//
// Public server details:
//   Host    : ldap.forumsys.com
//   Port    : 389
//   Base DN : dc=example,dc=com
//   Admin DN: cn=read-only-admin,dc=example,dc=com
//   Password: password
//
// Concepts covered:
//   1. Connecting to an LDAP server
//   2. Anonymous bind vs. authenticated bind
//   3. Searching with filters
//   4. Reading entry attributes
//   5. Verifying user credentials via bind
// ============================================================

using Novell.Directory.Ldap;

const string Host     = "ldap.forumsys.com";
const int    Port     = 389;
const string BaseDn   = "dc=example,dc=com";
const string AdminDn  = "cn=read-only-admin,dc=example,dc=com";
const string AdminPwd = "password";

// ── 1. CONNECT & ANONYMOUS BIND ─────────────────────────────
Console.WriteLine("=== 1. ANONYMOUS BIND ===");

// LdapConnection implements IDisposable — use 'using' to ensure disconnect
using (var conn = new LdapConnection())
{
    // ConnectAsync opens a TCP connection to the LDAP server
    await conn.ConnectAsync(Host, Port);
    Console.WriteLine($"Connected to {Host}:{Port}");

    // BindAsync with empty credentials = anonymous bind (read-only access)
    await conn.BindAsync(dn: string.Empty, passwd: string.Empty);
    Console.WriteLine("Anonymous bind succeeded");

    // Anonymous search — list all organisationalUnit entries under the base DN
    var filter  = "(objectClass=organizationalUnit)";
    var results = await conn.SearchAsync(BaseDn, LdapConnection.ScopeSub, filter,
                                         attrs: null, typesOnly: false);

    Console.WriteLine($"\nOrganisational Units under '{BaseDn}':");
    await foreach (var entry in results)
        Console.WriteLine($"  DN: {entry.Dn}");
}

// ── 2. AUTHENTICATED BIND ────────────────────────────────────
Console.WriteLine("\n=== 2. AUTHENTICATED BIND ===");

using (var conn = new LdapConnection())
{
    await conn.ConnectAsync(Host, Port);

    // BindAsync with a real DN and password — throws LdapException on bad credentials
    await conn.BindAsync(AdminDn, AdminPwd);
    Console.WriteLine($"Authenticated as: {AdminDn}");

    // ── 3. SEARCH ALL USERS ──────────────────────────────────
    Console.WriteLine("\n=== 3. SEARCH ALL USERS ===");

    // (objectClass=inetOrgPerson) matches standard user entries
    var userFilter = "(objectClass=inetOrgPerson)";

    // Request only specific attributes instead of fetching everything
    string[] requestedAttrs = ["uid", "cn", "mail", "sn"];

    var results = await conn.SearchAsync(BaseDn, LdapConnection.ScopeSub,
                                         userFilter, requestedAttrs, typesOnly: false);

    var users = new List<LdapEntry>();
    await foreach (var entry in results)
        users.Add(entry);

    Console.WriteLine($"Found {users.Count} user(s):\n");

    foreach (var user in users)
    {
        // GetStringValueOrDefault returns the attribute value, or the fallback if absent
        string uid  = user.GetStringValueOrDefault("uid",  "(none)")!;
        string cn   = user.GetStringValueOrDefault("cn",   "(none)")!;
        string sn   = user.GetStringValueOrDefault("sn",   "(none)")!;
        string mail = user.GetStringValueOrDefault("mail", "(none)")!;

        Console.WriteLine($"  UID : {uid}");
        Console.WriteLine($"  CN  : {cn}");
        Console.WriteLine($"  SN  : {sn}");
        Console.WriteLine($"  Mail: {mail}");
        Console.WriteLine($"  DN  : {user.Dn}");
        Console.WriteLine();
    }

    // ── 4. SEARCH A SPECIFIC USER ────────────────────────────
    Console.WriteLine("=== 4. SEARCH SPECIFIC USER (uid=einstein) ===");

    // Exact match filter on the 'uid' attribute
    var specificFilter  = "(uid=einstein)";
    var specificResults = await conn.SearchAsync(BaseDn, LdapConnection.ScopeSub,
                                                 specificFilter, attrs: null, typesOnly: false);

    LdapEntry? einstein = null;
    await foreach (var entry in specificResults)
    {
        einstein = entry;
        break; // we only need the first match
    }

    if (einstein is not null)
    {
        Console.WriteLine($"Found: {einstein.Dn}");

        // Iterate over ALL attributes returned for this entry
        Console.WriteLine("Attributes:");
        foreach (LdapAttribute attr in einstein.GetAttributeSet())
            Console.WriteLine($"  {attr.Name,-20} = {attr.StringValue}");
    }
    else
    {
        Console.WriteLine("User not found.");
    }

    // ── 5. VERIFY USER CREDENTIALS (bind as a specific user) ─
    Console.WriteLine("\n=== 5. VERIFY USER CREDENTIALS (bind as einstein) ===");

    // In real apps: to check a user's password, bind with their DN + supplied password.
    // Never store or compare passwords in plain text — always delegate to the LDAP server.
    string einsteinDn  = "uid=einstein,dc=example,dc=com";
    string einsteinPwd = "password"; // all test users share this password

    using var userConn = new LdapConnection();
    await userConn.ConnectAsync(Host, Port);

    try
    {
        await userConn.BindAsync(einsteinDn, einsteinPwd);
        Console.WriteLine($"Credential check PASSED for {einsteinDn}");
    }
    catch (LdapException ex)
    {
        // ResultCode 49 = Invalid Credentials
        Console.WriteLine($"Credential check FAILED (code {ex.ResultCode}): {ex.Message}");
    }
}

Console.WriteLine("\nDone.");
