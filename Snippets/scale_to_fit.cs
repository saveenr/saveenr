using SD=System.Drawing;

public static SD.Size ScaleToFit(SD.Size inputsize, SD.Size maxsize)
{
    double f = GetFitToAreaScalingFactor(inputsize.Width, inputsize.Height, maxsize.Width, maxsize.Height);
    var output = new SD.Size((int)(inputsize.Width * f), (int)(inputsize.Height * f));
    return output;
}

public static double GetFitToAreaScalingFactor(double inputwidth, double inputheight, double maxwidth, double maxheight)
{
    if ((inputwidth <= 0) || (inputheight <= 0) || (maxwidth <= 0) || (inputheight <= 0))
    {
        throw new System.ArgumentException("one of the input heightsor widths is negative");
    }

    double input_aspect_ratio = inputheight / inputwidth;
    double bounding_apect_ratio = maxheight / maxwidth;
    double scaling_factor;

    if (input_aspect_ratio <= bounding_apect_ratio)
    {
        scaling_factor = maxwidth / inputwidth;
    }
    else
    {
        scaling_factor = maxheight / inputheight;
    }

    return scaling_factor;
}