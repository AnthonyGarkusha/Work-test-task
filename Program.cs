//Задание 1
string Cipher(string input)
{
    string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    string out_lines = "";

    foreach(var line in lines)
    {
        var tmp = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //Если даные "ненадежные" тут можно делать проверку на соответствие строки требованиям и выкидывать exeption при неверном фомате данных
        if (tmp.Length != 2) break;
        int n = tmp[0].Length;

        int[] numbers = tmp[0].ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        string[] columns = new string[n];
        var Text = tmp[1].ToCharArray();
        int m = Text.Length;
        int counter = 0;
        for(int i = 0; i < m; i++)
        {
            columns[counter % n] += Text[i++];
            if(i<m) 
                columns[counter++ % n] += Text[i++];
            if(i<m)
                columns[counter++ % n] += Text[i];
        }
        for (int i = 0; i < n; i++)
        {
            int place = Array.IndexOf(numbers, i+1);
            out_lines += columns[place];
        }
        out_lines += '\n';
    }
    return out_lines;
}
// Задание 2
string derivative(string input_string, int m = 10, int amount = 3)
{
    int[] output = new int[3];

    int[][] deltas = new int[m][];

    string[] split = input_string.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    deltas[0] = new int[split.Length];
    //Проверка на то, что в вводе все даниные - цыфры и нету ничего лишнего
    for (int i = 0; i < split.Length; i++)
    {
        if(!int.TryParse(split[i], out deltas[0][i]))
        {
            return "Incorrect input format";
        }
    }

    for (int counter = 1; counter < m && counter < deltas[0].Length; counter++)
    {
        int Num = deltas[0].Length - counter;
        deltas[counter] = new int[Num];
        for (int i = 0; i < Num; i++)
        {
            deltas[counter][i] = deltas[counter - 1][i + 1] - deltas[counter - 1][i];
        }
        //Если надо давать ответ, только если d_n = [0..]
        if (deltas[counter].Min() == 0 && deltas[counter].Max() == 0) 

        //Если надо находить ответ даже для недостаточного масива типа "1 3 9 27" (вывод "65 131 233") или "1 2" -> "3 4 5", "42" -> "42 42 42"
        //if (deltas[counter].Min() ==  deltas[counter].Max()) 
        {
            int[][] new_deltas = new int[counter+1][];
            new_deltas[counter] = new int[amount];
            Array.Fill(new_deltas[counter], deltas[counter][0]);
            for(int i = counter-1; i >= 0; i--)
            {
                new_deltas[i] = new int[amount];
                new_deltas[i][0] = deltas[i][^1] + new_deltas[i + 1][0];

                for (int j = 1; j < amount; j++)
                {
                    new_deltas[i][j] = new_deltas[i][j-1] + new_deltas[i+1][j]; 
                }
            }
            output = new_deltas[0];

            break;
        }

    }

    string out_string = "";
    for (int i = 0; i < amount-1; i++)
    {
        out_string += output[i].ToString() + ' ';
    }
    out_string += output[amount-1].ToString();

    return out_string;
}


var Test_Inp_1 = "41325 INCOMPLETECOLUMNARWITHALTERNATINGSINGLELETTERSANDDIGRAPHS  \n" +
    "12 HELLOWORLD\n" +
    "3412 THISISJUSTATEST\n" +
    "165432 WORKSMARTNOTHARD\n" +
    "231 LLOHE\n" +
    "";


var Test_out_1 = Cipher(Test_Inp_1);
var Test_exp_1 = "CECRTEGLENPHPLUTNANTEIOMOWIRSITDDSINTNALINESAALEMHATGLRGR\n" +
    "HELOORDLWL\n" +
    "SITASTTHJUESIST\n" +
    "WONOTARDMRKSHART\n" +
    "HELLO\n" +
    "";


var Test_Inp_2_0 = "1 3 6 10 15";
var Test_exp_2_0 = "21 28 36";
var Test_out_2_0 = derivative(Test_Inp_2_0);

var Test_Inp_2_1 = "12 14 16 18 20";
var Test_exp_2_1 = "22 24 26";
var Test_out_2_1 = derivative(Test_Inp_2_1);

var Test_Inp_2_2 = "15 32 57 90 131 180";
var Test_exp_2_2 = "237 302 375";
var Test_out_2_2 = derivative(Test_Inp_2_2);

var Test_Inp_2_3 = "1 2";
var Test_exp_2_3 = "3 4 5";
var Test_out_2_3 = derivative(Test_Inp_2_3);


var Test_Inp_2_4 = "1 3 9 27";
var Test_exp_2_4 = "65 131 233";
var Test_out_2_4 = derivative(Test_Inp_2_4);




Console.WriteLine(Test_out_1);

Console.WriteLine(Test_out_1
               == Test_exp_1);

Console.WriteLine(Test_out_2_0);
Console.WriteLine(Test_out_2_0
               == Test_exp_2_0);
                           
Console.WriteLine(Test_out_2_1);
Console.WriteLine(Test_out_2_1
               == Test_exp_2_1);

Console.WriteLine(Test_out_2_2);
Console.WriteLine(Test_out_2_2
               == Test_exp_2_2);

Console.WriteLine(Test_out_2_3);
Console.WriteLine(Test_out_2_3
               == Test_exp_2_3);

Console.WriteLine(Test_out_2_4);
Console.WriteLine(Test_out_2_4
               == Test_exp_2_4);


