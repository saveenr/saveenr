namespace DemoCreateSlideShow
{
    public static class DisplayResolutions
    {
        // 16:9
        public static DisplayResolution HD1080 = new DisplayResolution("HD1080", 16,9,1920,1080);
        public static DisplayResolution HD720= new DisplayResolution("HD720", 16, 9, 1280,720);

        // 16:10
        public static DisplayResolution WQUXGA = new DisplayResolution("WQUXGA", 16, 10, 3840, 2400);
        public static DisplayResolution WQXGA = new DisplayResolution("WQXGA", 16, 10, 2560, 1600);
        public static DisplayResolution WUXGA = new DisplayResolution("WUXGA", 16, 10, 1920, 1200);
        public static DisplayResolution WUXGA_PLUS = new DisplayResolution("WUXGA+", 16, 10, 1680, 1050);
        public static DisplayResolution RES_1440by900 = new DisplayResolution(null, 16, 10, 1440, 900);
        public static DisplayResolution WXGA = new DisplayResolution("WXGA", 16, 10, 1280, 800);
        public static DisplayResolution CGA = new DisplayResolution("CGA", 16, 10, 320, 200);

        // 4:3
        public static DisplayResolution UXGA = new DisplayResolution("UXGA", 4, 3, 1600, 1200);
        public static DisplayResolution SXGA_PLUS = new DisplayResolution("SXGA+", 4, 3, 1400, 1050);
        public static DisplayResolution RES_1280by960_a= new DisplayResolution(null, 4, 3, 1280, 960);
        public static DisplayResolution XGA= new DisplayResolution("XGA", 4, 3, 1024, 768);
        public static DisplayResolution SVGA = new DisplayResolution("SVGA", 4, 3, 800, 600);
        public static DisplayResolution VGA = new DisplayResolution("VGA", 4, 3, 640, 480);
        public static DisplayResolution QVGA = new DisplayResolution("QVGA", 4, 3, 320, 240);

        // 5:4
        public static DisplayResolution QSXGA = new DisplayResolution("QSXGA", 5, 4, 2560, 2048);
        public static DisplayResolution SXGA = new DisplayResolution("SXGA", 5, 4, 1280, 1024);


    }
}