namespace HelloWorld.Interfaces.Extensibility;

public static class App
{
    public static void Run()
    {
        var dbMigrator = new DbMigrator(new FileLogger("C:\\Temp\\log.txt"));
        dbMigrator.Migrate();

        var dbMigrator2 = new DbMigrator(new FileLogger("C:\\Temp\\log2.txt"));
        dbMigrator2.Migrate();

        var dbMigrator3 = new DbMigrator(new ConsoleLogger());
        dbMigrator3.Migrate();
    }
}