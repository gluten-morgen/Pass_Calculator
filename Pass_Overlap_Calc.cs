using System;


class Pass_Overlap_Calc
{
    public float L, nom_pitch, pass_width;
    public float tolerance_min, tolerance_max;

    public static int iteration_cntr;
    public static float pass;

    public Pass_Overlap_Calc()
    {
        L = 0.0f;
        nom_pitch = 0.0f;
        pass = 0.0f;
        pass_width = 0.0f;

        tolerance_min = 0.0f;
        tolerance_max = 0.0f;
    }

    public void Input()
    {
        Console.Write("L :");
        L = (float)Convert.ToDouble(Console.ReadLine());
        L = (float)Math.Round(L, 2);

        Console.Write("pitch: ");
        nom_pitch = (float)Convert.ToDouble(Console.ReadLine());
        nom_pitch = (float)Math.Round(nom_pitch, 2);

        Console.Write("pass width: ");
        pass_width = (float)Convert.ToDouble(Console.ReadLine());
        pass_width = (float)Math.Round(pass_width, 2);

        Console.Write("Tolerance min: ");
        tolerance_min = (float)Convert.ToDouble(Console.ReadLine());
        tolerance_min = (float)Math.Round(tolerance_min, 2);

        Console.Write("Tolerance max: ");
        tolerance_max = (float)Convert.ToDouble(Console.ReadLine());
        tolerance_max = (float)Math.Round(tolerance_max, 2);
    }

    public void CalculatePasses()
    {
        float error_pitch, error_length, pitch_temp, pitch_final;

        float L_calc = L - pass_width;

        pass = L_calc / nom_pitch;
        pass += 1;

        pass = (float)Math.Round(pass);
        pitch_temp = L_calc / pass;

        iteration_cntr = 0;
        pitch_final = Iterations(pitch_temp);
        pitch_final = (float)Math.Round(pitch_final, 2);

        error_length = Math.Abs(((pitch_final * pass) + pass_width) - L);
        error_length = (float)Math.Round(error_length, 2);

        /*if (pitch_final == -0.1f) pass++;
        else if (pitch_final == 0.1f) pass--;*/

        Console.WriteLine("{0} passes at {1}mm pitch, with {2}mm error in length.", pass, pitch_final, error_length);

        
    }

    public float Iterations(float pitch_calc)
    {
        float error_pitch, error_length;

        error_pitch = (pitch_calc - nom_pitch) / nom_pitch;
        //error_length = ((pitch_calc * pass) + pass_width) - L;

        error_pitch = (float)Math.Round(error_pitch, 2);
        //error_length = (float)Math.Round(error_length, 2);

        if (In_tolerance(error_pitch))
        {
            return pitch_calc;
        }
        else
        {
            if (error_pitch <= tolerance_min)
                error_pitch = tolerance_min - error_pitch;
            else if (error_pitch >= tolerance_max)
                error_pitch = error_pitch - tolerance_max;

            pitch_calc = pitch_calc * (1 + error_pitch);
            pitch_calc = (float)Math.Round(pitch_calc, 2);

            error_length = ((pitch_calc * pass) + pass_width) - L;
            error_length = (float)Math.Round(error_length, 2);

            iteration_cntr++;

            if (iteration_cntr % 2 == 0)
            {
                if (iteration_cntr >= 6) return 0.0f;
            }

            if (error_length >= 1)
            {
                pass -= 1;
            }
            else if (error_length <= -1)
            {
                pass += 1;
            }

            if (In_tolerance(error_pitch) && Math.Abs(error_length) < 1) return pitch_calc;

            return Iterations((L - pass_width) / pass);
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
        calc.CalculatePasses();
    }
}