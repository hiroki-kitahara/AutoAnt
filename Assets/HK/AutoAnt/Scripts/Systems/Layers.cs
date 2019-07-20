namespace HK.AutoAnt
{
    /// <summary>
    /// AutoAntで利用するレイヤー
    /// </summary>
    public static class Layers
    {
        public static class Id
        {
            public const int Default        = 0;
            public const int TransparentFX  = 1;
            public const int IgnoreRaycast  = 2;
            public const int Water          = 4;
            public const int UI             = 5;
            public const int Postprocessing = 8;
        }

        public static class Mask
        {
            public const int Default        = (1 << Id.Default);
            public const int TransparentFX  = (1 << Id.TransparentFX);
            public const int IgnoreRaycast  = (1 << Id.IgnoreRaycast);
            public const int Water          = (1 << Id.Water);
            public const int UI             = (1 << Id.UI);
            public const int Postprocessing = (1 << Id.Postprocessing);
        }
    }
}
