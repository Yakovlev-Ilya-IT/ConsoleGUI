namespace ConsoleGUILib.Data
{
    public class Cell
    {
        public Cell()
        {
            Content = ' ';
            Foreground = ConsoleColor.White;
            Background = ConsoleColor.Black;
        }

        public Cell(char content) : this()
        {
            Content = content;
        }

        public Cell(char content, ConsoleColor foreground, ConsoleColor background) : this(content)
        {
            Foreground = foreground;
            Background = background;
        }

        public char Content { get; }

        public ConsoleColor Foreground { get; }
        public ConsoleColor Background { get; }

        public Cell WithContent(char content) => new Cell(content, Foreground, Background);
        public Cell WithForeground(ConsoleColor foreground) => new Cell(Content, foreground, Background);
        public Cell WithBackground(ConsoleColor background) => new Cell(Content, Foreground, background);

        public static bool operator ==(Cell first, Cell second)
        {
            if((object) first == null)
                return (object) second == null;

            if ((object) second == null)
                return (object)first == null;

            return first.Content == second.Content && first.Foreground == second.Foreground && first.Background == second.Background;
        }
        public static bool operator !=(Cell first, Cell second) => !(first == second);

        public override bool Equals(object obj) => obj is Cell cell && this == cell;

        public override int GetHashCode() => HashCode.Combine(Content, Foreground, Background);
    }
}
