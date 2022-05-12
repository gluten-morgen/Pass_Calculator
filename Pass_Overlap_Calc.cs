using System;


class Pass_Overlap_Calc
{
    public double L, pitch, pass, pass_width;
    public double tolerance_min, tolerance_max;

    public Pass_Overlap_Calc()
    {
        L = 0.0f;
        pitch = 0.0f;
        pass = 0.0f;
        pass_width = 0.0f;

        tolerance_min = 0.0f;
        tolerance_max = 0.0f;
    }

    public void Input()
    {
        Console.Write("L :");
        L = (float)Convert.ToDouble(Console.ReadLine());
        L = Math.Round(L, 2);

        Console.Write("pitch: ");
        pitch = (float)Convert.ToDouble(Console.ReadLine());
        pitch = Math.Round(pitch, 2);

        Console.Write("pass width: ");
        pass_width = (float)Convert.ToDouble(Console.ReadLine());

        Console.Write("Tolerance min: ");
        tolerance_min = (float)Convert.ToDouble(Console.ReadLine());
        tolerance_min = Math.Round(tolerance_min, 2);

        Console.Write("Tolerance max: ");
        tolerance_max = (float)Convert.ToDouble(Console.ReadLine());
        tolerance_max = Math.Round(tolerance_max, 2);
    }

    public void Calculate(double pitch)
    {
        double error_pitch, error_length, pitch_1, final;

        double L_calc = L - pass_width;

        pass = L_calc / pitch;
        pass += 1;

        pass = Math.Round(pass);
        pitch_1 = L_calc / pass;

        final = Iterations(pitch_1);
        final = Math.Round(final, 2);

        Console.WriteLine("{0} passes at {1}mm pitch.", pass, final);

        
    }

    public double Iterations(double pitch_1)
    {
        double error_pitch = (pitch_1 - pitch) / pitch;
        double error_length = Math.Abs(((pitch_1 * pass) + pass_width) - L);

        error_pitch = Math.Round(error_pitch, 2);
        error_length = Math.Round(error_length, 2);

        if (In_tolerance(error_pitch))
        {
            return pitch_1;
        }
        else
        {
            if (error_pitch < tolerance_min)
                error_pitch = tolerance_min - error_pitch;
            else if (error_pitch > tolerance_max)
                error_pitch = error_pitch - tolerance_max;

            pitch_1 = pitch_1 * (1 + error_pitch);
            pitch_1 = Math.Round(pitch_1, 2);

            error_pitch = (pitch_1 - pitch) / pitch;
            error_length = Math.Abs(((pitch_1 * pass) + pass_width) - L);

            return Iterations(pitch_1);
        }
    }

    public bool In_tolerance(double delta)
    {
        delta = Math.Round(delta, 2);
        return (delta >= tolerance_min) && (delta <= tolerance_max);
    }


    public static void Main()
    {
        Pass_Overlap_Calc calc = new();

        calc.Input();
        calc.Calculate(3.2);
    }
}