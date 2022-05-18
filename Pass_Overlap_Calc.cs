

public partial class Pass_Overlap_Calc
{
    private void CalculatePasses()
    {
        float pitch_temp;

        float L_calc = L - pass_width;

        pass = L_calc / nom_pitch;
        pass += 1;

        pass = (float)Math.Round(pass);
        pitch_temp = L_calc / pass;

        iteration_cntr = 1;
        pitch_final = CalculatePitch(pitch_temp);
        pitch_final = (float)Math.Round(pitch_final, 2);
    }

    private float CalculatePitch(float pitch_calc)
    {
        float error_pitch, error_length;

        error_pitch = (pitch_calc - nom_pitch) / nom_pitch;
        error_pitch = (float)Math.Round(error_pitch, 2);


        if (Pitch_InTolerance(error_pitch))
        {
            error_length = ((pitch_calc * pass) + pass_width) - L;
            error_length = (float)Math.Round(error_length, 2);


            if (Length_InTolerance(error_length)) return pitch_calc;
            else
            {
                if (error_length >= length_tolerance_max) pass -= 1;
                else if (error_length <= length_tolerance_min) pass += 1;

                iteration_cntr += 1;
                return CalculatePitch((L - pass_width) / pass);
            }
        }
        else
        {
            if (error_pitch <= pitch_tolerance_min)
                error_pitch = pitch_tolerance_min - error_pitch;
            else if (error_pitch >= pitch_tolerance_max)
                error_pitch = error_pitch - pitch_tolerance_max;

            pitch_calc = pitch_calc * (1 + error_pitch);
            pitch_calc = (float)Math.Round(pitch_calc, 2);

            error_length = ((pitch_calc * pass) + pass_width) - L;
            error_length = (float)Math.Round(error_length, 2);


            if (Pitch_InTolerance(error_pitch) && Length_InTolerance(error_length)) return pitch_calc;
            if (iteration_cntr >= max_iter) return 0.0f;


            if (error_length >= length_tolerance_max) pass -= 1;  
            else if (error_length <= length_tolerance_min) pass += 1;
       

            iteration_cntr += 1;
            return CalculatePitch((L - pass_width) / pass);
        }
    }

    private bool Pitch_InTolerance(float delta)
    {
        delta = (float)Math.Round(delta, 2);
        return (delta >= pitch_tolerance_min) && (delta <= pitch_tolerance_max);
    }

    private bool Length_InTolerance(float delta)
    {
        delta = (float)Math.Round(delta, 2);
        return (delta >= length_tolerance_min) && (delta <= length_tolerance_max);
    }





    public void DisplaySolution()
    {
        float error_pitch, error_length;

        error_pitch = (pitch_final - nom_pitch) / nom_pitch;
        error_pitch = (float)Math.Round(error_pitch, 2);

        error_length = Math.Abs(((pitch_final * pass) + pass_width) - L);
        error_length = (float)Math.Round(error_length, 2);

        Console.Write("\n\n");
        Console.Write("\n\t*************************************\t\n\n");

        if (pitch_final > 0.0f)
        {
            Console.WriteLine("{0} passes at {1}mm pitch, {2}% pitch error with {3}mm error in length.", pass, pitch_final, error_pitch * 100, error_length);
            Console.WriteLine("\nSolution converged in {0} iteration(s).", iteration_cntr);
        }
        else
        {
            Console.WriteLine("\nCalculation error: Solution failed to converge in {0} (max) iterations.", iteration_cntr);
        }
        Console.Write("\n\t*************************************\t\n");
    }

    public void Calculate()
    {
        CalculatePasses();
    }
}
