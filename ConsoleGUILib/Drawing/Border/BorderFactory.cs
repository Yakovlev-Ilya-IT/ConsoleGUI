namespace ConsoleGUILib.Drawing
{
    public static class BorderFactory
    {
        private static Border _noneBorder = new NoneBorder();
        private static Border _singleBorder = new SingleBorder();
        private static Border _doubleBorder = new DoubleBorder();

        public static Border Get(BorderStyle borderStyle)
        {
            switch (borderStyle)
            {
                case BorderStyle.None:
                    return _noneBorder;

                case BorderStyle.Single:
                    return _singleBorder;

                case BorderStyle.Double:
                    return _doubleBorder;

                default:
                    throw new ArgumentException(nameof(borderStyle));
            }
        }
    }
}
