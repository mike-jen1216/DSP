public static class SingletonDTO
{
    private static object _instance = new object();

    public static object Instance => _instance;
}