Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore

Scaffold-DbContext "Data Source=BACH;Initial Catalog=OnTap;Persist Security Info=True;User ID=sa;Password=2002;
TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer" -OutputDir Models/EF -f
