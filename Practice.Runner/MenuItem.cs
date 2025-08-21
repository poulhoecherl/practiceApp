namespace Practice.Runner
{
    public class MenuItem
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public Action? Action { get; set; } // Or a sub-menu identifier

        public override string ToString() => Name;
    }
}
