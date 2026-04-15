namespace lab2CompMath
{
	public static class Methods
	{
        public static void ReverseStroke(float[,] matrix, int row, int column, bool flag, int[]? vector = default)
        {
            float[] x = new float[row];

            for (int i = row - 1; i >= 0; i--) // i - строки (с конца)
            {
                float sum = 0;
                for (int j = i + 1; j < row; j++)  // j - столбцы (учитываем уже найденные переменные)
                {
                    sum += matrix[i, j] * x[j];
                }

                x[i] = (matrix[i, column - 1] - sum) / matrix[i, i];
            }
            float[] _x = new float[row];
            if (flag)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (vector?[j] == i)
                        {
                            _x[i] = x[j];
                        }
                    }
                }
                PrintResult(_x);
            }
            else
                PrintResult(x);
            
        }

        public static void PrintResult(float[] x)
        {
            Console.Write("Найденные значения: ");
            int t = 1;
            bool flag = false;
            foreach (float o in x)
            {
                if (float.IsNaN(o))
                    flag = true;
                Console.Write($"x{t} = {o}; ");
                t++;
            }
            if (flag)
            {
                Console.WriteLine("\nМетод расходится. Решение не найдено");
            }
        }

        public static void GaussNoMainElement(float[,]? matrix)
        {
            //схема единственного деления
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов
            for (int n = 0; n < column - 2; n++) //столбцы       N-2 - так как расширенная матрица
            {
                for (int i = n + 1; i < row; i++) //строки
                {
                    //сейчас я на второй строке
                    float koeff = matrix[i, n] / matrix[n, n];
                    for (int j = 0; j < column; j++) //элементы
                    {
                        matrix[i, j] = matrix[i, j] - koeff * matrix[n, j];
                    }
                }
                
            }

            ReverseStroke(matrix, row, column, false);

        }

        public static void GaussWithMainElementByColumn(float[,]? matrix)
        { 
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов
			for (int n = 0; n < column - 2; n++) //столбцы       N-2 - так как расширенная матрица
			{

                float max_element = -1000;
                int number_row = 0;
                float[] copy_row = new float[column];
                for (int k = n; k < row; k++) //поиск макс элемента в столбце на каждом шаге в зависимости от номера столбца
                {
                   
                    if (Math.Abs(matrix[k, n]) > max_element)
                    {
                        max_element = Math.Abs(matrix[k, n]);
                        number_row = k;
                        for (int j = 0; j < column; j++)
                        {
                            //сейчас я по элементам k- строки
                            copy_row[j] = matrix[k, j];
                        }
                        //теперь у меня есть строка которую надо поставить на текущую позицию и ее номер
                    }
                }
                //теперь у меня есть номер строки которую надо поставить на место текущей

                //сейчас я на нулевой строке
                for (int j = 0; j < column; j++)
                {
                    //сейчас я по элементам нулевой строки
                    matrix[number_row, j] = matrix[n, j];
                    matrix[n, j] = copy_row[j];
                }
                //теперь в первой строке макс значение и строки поменяны местами

                for (int i = n + 1; i < row; i++) //строки
                {
                    //сейчас я на второй строке
                    float koeff = matrix[i, n] / matrix[n, n];
                    for (int j = 0; j < column; j++) //элементы
                    {
                        matrix[i, j] = matrix[i, j] - koeff * matrix[n, j];
                    }
                }
            }
            ReverseStroke(matrix, row, column, false);
            
        }

        public static void GaussWithMainElementByRow(float[,]? matrix)
        {
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов
            int[] vector = new int[row];
            for (int i = 0; i < row; i++)
                vector[i] = i;


            for (int n = 0; n < column - 2; n++) //столбцы       N-2 - так как расширенная матрица
            {

                float max_element = -1000;
                int column_number = 0;
                float[] copy_column = new float[row];

                for (int i = n; i < row; i++)
                {
                    if (Math.Abs(matrix[n, i]) > max_element)
                    {
                        max_element = Math.Abs(matrix[n, i]);
                        column_number = i;
                    }
                }
                //нужно поменять столбцы местами

                for (int j = 0; j < row; j++) //записываем в копи столбец значения 
                {
                    copy_column[j] = matrix[j, n]; //копи 0 столбец
                    matrix[j, n] = matrix[j, column_number]; //запись в 0 макс
                    matrix[j, column_number] = copy_column[j]; //запись в с макс 0 столбца
                }
                //Console.WriteLine($"[{vector[0]} {vector[1]} {vector[2]}]");
                if (vector[n] != column_number)
                {
                    int copy = vector[n];
                    vector[n] = vector[column_number];
                    vector[column_number] = copy;
                }

                for (int i = n + 1; i < row; i++) //строки
                {
                    //сейчас я на второй строке
                    float koeff = matrix[i, n] / matrix[n, n];
                    for (int j = 0; j < column; j++) //элементы
                    {
                        matrix[i, j] = matrix[i, j] - koeff * matrix[n, j];
                    }
                }

            }
            ReverseStroke(matrix, row, column, true, vector);
        }

        public static void GaussWithMainElementByAllMatrix(float[,]? matrix)
        {
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов
            int[] vector = new int[row];
            for (int i = 0; i < row; i++)
                vector[i] = i;
            for (int n = 0; n < column - 2; n++) //столбцы       N-2 - так как расширенная матрица
            {

                float max_element = -1000;
                (int R, int C) number = (0, 0);
                float[] copy_row = new float[column];
                float[] copy_column = new float[row];

                for (int i = n; i < row; i++)
                {
                    for (int j = n; j < row; j++)
                    {
                        if (Math.Abs(matrix[i, j]) > max_element)
                        {
                            max_element = Math.Abs(matrix[i, j]);
                            number = (i, j);
                        }
                    }
                }
                //теперь у меня есть максимальный элемент
                //сначала меняем строки местами

                for (int j = 0; j < column; j++) //записываем в копи строку значения 
                {
                    copy_row[j] = matrix[n, j]; //копи 0 строка
                    matrix[n, j] = matrix[number.R, j]; //запись в 0 макс
                    matrix[number.R, j] = copy_row[j]; //запись в с макс 0 строки
                }

                //теперь у меня строки поменяны местами
                //нужно поменять столбцы местами
                for (int j = 0; j < row; j++) //записываем в копи столбец значения 
                {
                    copy_column[j] = matrix[j, n]; //копи 0 столбец
                    matrix[j, n] = matrix[j, number.C]; //запись в 0 макс
                    matrix[j, number.C] = copy_column[j]; //запись в с макс 0 столбца
                }

                if (vector[n] != number.C)
                {
                    int copy = vector[n];
                    vector[n] = vector[number.C];
                    vector[number.C] = copy;
                }

                for (int i = n + 1; i < row; i++) //строки
                {
                    //сейчас я на второй строке
                    float koeff = matrix[i, n] / matrix[n, n];
                    for (int j = 0; j < column; j++) //элементы
                    {
                        matrix[i, j] = matrix[i, j] - koeff * matrix[n, j];
                    }
                }
            }
            ReverseStroke(matrix, row, column, true, vector);
        }

        public static void SimpleIteration(float[,]? matrix)
        {
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов

            bool flag = true;
            int.TryParse(Console.ReadLine(), out int a);
            double eps = 1e-3;
            int iteration = 0;
            float norm_matrix = -10000;
            float[] old_x = new float[row];
            float[] new_x = new float[row];
            float[] error1 = new float[row];
            bool[] error2 = new bool[row];

            for (int i = 0; i < row; i++)
            {
                if (matrix[i, i] == 0)
                {
                    Console.WriteLine("Найден нулевой диагональный элемент! Решение невозможно!");
                    flag = false;
                    break;
                }

                old_x[i] = matrix[i, column - 1] / matrix[i, i]; //начальные приближения (деления на ноль нет 100%)
                float sum = 0;
                for (int j = 0; j < row; j++)
                {
                    sum += Math.Abs(matrix[i, j]);
                }
                if (2 * Math.Abs(matrix[i, i]) <= sum) //условие для поиска решения по диагональным элементам (следствие 2.5)
                {
                    Console.WriteLine($"Обнаружено не диагональное преобладание в строке {i+1}! Метод может расходится.");
                }
            }

            //поиск нормы матрицы
            for (int i = 0; i < row; i++)
            {
                float norm = 0;
                for (int j = 0; j < row; j++)
                {
                    if (i != j) //диагональные пропускаем
                    {
                        norm += -matrix[i, j] / matrix[i, i];
                    }
                }

                norm_matrix = norm > norm_matrix ? norm : norm_matrix;

            }
            //Console.WriteLine(norm_matrix);
            if (flag)
            {
                while (iteration < 1000)
                {
                    bool flag_iteration = false;
                    for (int i = 0; i < row; i++) //одна итерация
                    {
                        //иду по строкам
                        //выражаю x
                        float summary = 0;
                        for (int j = 0; j < row; j++)
                        {
                            summary += matrix[i, j] * old_x[j];
                        }
                        float _x = (matrix[i, column - 1] - (summary - matrix[i, i] * old_x[i])) / matrix[i, i]; //новое приближение
                        error1[i] = Math.Abs(_x - old_x[i]); //2.9
                        error2[i] = Math.Abs(_x - old_x[i]) <= (1 - Math.Abs(norm_matrix)) * eps / Math.Abs(norm_matrix); //2.8
                        new_x[i] = _x;
                    }
                    old_x = new_x;
                    //нужно проверить погрешности
                    for (int i = 0; i < row; i++) //проверка 2.9
                    {
                        if (error1[i] > eps)
                        {
                            flag_iteration = true;
                        }
                    }
                    
                    for (int i = 0; i < row; i++) //проверка 2.8
                    {
                        if (!error2[i])
                        {
                            flag_iteration = true;
                        }
                    }
                    if (!flag_iteration)
                    {
                        PrintResult(old_x);
                        break;
                    }

                    iteration++;
                }
            }
            else
                Console.WriteLine("Решение не найдено!");
        }

        public static void Seidel(float[,]? matrix)
        {
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов

            bool flag = true;
            double eps = 1e-3;
            int iteration = 0;
            float norm_matrix = -10000;
            float[] old_x = new float[row];

            float[] error1 = new float[row];
            bool[] error2 = new bool[row];

            for (int i = 0; i < row; i++)
            {
                if (matrix[i, i] == 0)
                {
                    Console.WriteLine("Найден нулевой диагональный элемент! Решение невозможно!");
                    flag = false;
                    break;
                }

                old_x[i] = matrix[i, column - 1] / matrix[i, i]; //начальные приближения (деления на ноль нет 100%)
                float sum = 0;
                for (int j = 0; j < row; j++)
                {
                    sum += Math.Abs(matrix[i, j]);
                }
                if (2 * Math.Abs(matrix[i, i]) <= sum) //условие для поиска решения по диагональным элементам (следствие 2.5)
                {
                    Console.WriteLine($"Обнаружено не диагональное преобладание в строке {i + 1}! Метод может расходится.");
                }
            }

            //поиск нормы матрицы
            for (int i = 0; i < row; i++)
            {
                float norm = 0;
                for (int j = 0; j < row; j++)
                {
                    if (i != j) //диагональные пропускаем
                    {
                        norm += -matrix[i, j] / matrix[i, i];
                    }
                }

                norm_matrix = norm > norm_matrix ? norm : norm_matrix;

            }
            //Console.WriteLine(norm_matrix);
            if (flag)
            {
                while (iteration < 1000)
                {
                    bool flag_iteration = false;
                    for (int i = 0; i < row; i++) //одна итерация
                    {
                        //иду по строкам
                        //выражаю x
                        float summary = 0;
                        for (int j = 0; j < row; j++)
                        {
                            summary += matrix[i, j] * old_x[j];
                        }
                        float _x = (matrix[i, column - 1] - (summary - matrix[i, i] * old_x[i])) / matrix[i, i]; //новое приближение
                        error1[i] = Math.Abs(_x - old_x[i]); //2.9
                        error2[i] = Math.Abs(_x - old_x[i]) <= (1 - Math.Abs(norm_matrix)) * eps / Math.Abs(norm_matrix); //2.8
                        old_x[i] = _x;
                    }
                 
                    //нужно проверить погрешности
                    for (int i = 0; i < row; i++) //проверка 2.9
                    {
                        if (error1[i] > eps)
                        {
                            flag_iteration = true;
                        }
                    }

                    for (int i = 0; i < row; i++) //проверка 2.8
                    {
                        if (!error2[i])
                        {
                            flag_iteration = true;
                        }
                    }
                    if (!flag_iteration)
                    {
                        PrintResult(old_x);
                        break;
                    }

                    iteration++;
                }
            }
            else
                Console.WriteLine("Решение не найдено!");
        }

        public static void RunningAlgorithm(float[,]? matrix)
        {
            int row = matrix.GetLength(0); //кол-во строк
            int column = matrix.GetLength(1); //кол-во столбцов
            float[][] koeff = new float[column - 1][];
            for (int i = 0; i < koeff.Length; i++)
            {
                koeff[i] = new float[2];
            }
            float A, B, e;
            //прямой ход
            for(int i = 0; i < row; i++)
            {
                //сейчас я на нулевой строке
                if (i == 0)
                {
                    A = -matrix[i, i + 1] / matrix[i, i];
                    B = matrix[i, column - 1] / matrix[i, i];
                }
                else if (i == row - 1)
                {
                    A = 0;
                    B = (matrix[i, column - 1] - matrix[i, i - 1] * koeff[i - 1][1]) /
                        (matrix[i, i - 1] * koeff[i - 1][0] + matrix[i, i]);
                }
                else
                {
                    e = matrix[i, i - 1] * koeff[i - 1][0] + matrix[i, i]; //+
                    A = -matrix[i, i + 1] / e; //+
                    B = (matrix[i, column - 1] - matrix[i, i - 1] * koeff[i - 1][1]) / e;
                }
                koeff[i][0] = A;
                koeff[i][1] = B;
            }

            //обратный ход
            float[] x = new float[column - 1];

            x[column - 2] = koeff[column - 2][1];
            for (int i = column - 3; i >= 0; i--)
            {
                x[i] = koeff[i][0] * x[i + 1] + koeff[i][1];
            }

            PrintResult(x);
        }
    }
}
