﻿using System.Text;

namespace Library
{
    public static class CsvProcessing
    {
        static string fPath;
        // свойство для назначения корректного пути
        public static string FPath
        {
            get => fPath;
            set => fPath = (value != null && value.Length != 0) ? value : throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// Метод для считывания файла и проверки его на корректность
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string[] Read()
        {
            string[] massivStrings = File.ReadAllLines(fPath);
            if (massivStrings.Length != 0 && massivStrings[0] != null) // если файл не пустой
            {
                CheakString(massivStrings);
            }
            else
            {
                throw new ArgumentNullException();
            }
            return massivStrings;
        }
        /// <summary>
        /// Алгоритм проверки файла на корректность данных
        /// </summary>
        /// <param name="massivStrings">массив строк</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheakString(string[] massivStrings)
        {
            for (int v = 0; v < massivStrings.Length; v++) // проверяем каждую строку на корректность данных
            {
                if (massivStrings[v].Length == 0)
                {
                    continue;
                }
                // этот цикл нужен в случае пробелов в конце строки. однако я не пользуюсь им, так как
                // записываю строки подряд \n. но он у меня есть в случае другой задачи
                // в этой я считаю, что наличие символа НЕ ";" в конце строки - являяется исключением
                /* if (massivStrings[v][^1] != ';')
                {
                    while (massivStrings[v][^1] != ';')
                    {
                        massivStrings[v] = massivStrings[v][..^1];
                    }
                } */

                string someString = massivStrings[v];
                int i = 0; // индекс для прохода по строке
                int kolProverka = 0; // количество разделенных элементов в строке
                if (someString[^1] != ';')
                {
                    // в таком случае запись данных в файле некорректна
                    throw new ArgumentNullException();
                }
                while (i < someString.Length)
                {
                    // если идет ячейка с =", то мы переходим к шагу с кавычками
                    if ((someString[i] == '=') && (someString[i + 1] == '"'))
                    {
                        i++;
                    }
                    if (kolProverka == 26) // то выборок в строк больше 26, что не соотвествует первой строке, завершаем цикл, чтобы выкинуть предупреждение ниже
                    {
                        kolProverka++; // чтобы сработало предупреждение ниже
                    }
                    if (someString[i] == '\"')
                    {
                        if (kolProverka == 25)
                        { // если последняя выборка заходит с '"', то предпоследний элемент должен быть '"'
                            if (someString[^2] != '"')
                            {
                                // в таком случае запись данных в файле некорректна
                                throw new ArgumentNullException();
                            }
                        }
                        i++;
                        // цикл для сборки одной из выборок
                        while (someString[i] != '\"' || someString[i + 1] != ';')
                        {
                            i++;
                        }
                        i += 2; // сдвигаемся на после ';'
                        kolProverka++;
                        continue;
                    }
                    // берем выборку, если она идет без кавычек
                    while (someString[i] != ';')
                    {
                        i++;
                    }
                    kolProverka++; // занесли выборку
                    i++; // сдвигаемся с ";"
                    continue;
                }
                if (kolProverka != 26) // если записалось не 26, то данные в файле записаны неверно, предупреждаем об этом пользователя
                {
                    throw new ArgumentNullException();
                }
            }
        }
        /// <summary>
        /// Отвечает за рассортировку всех данных, элементов по массивам. Возвращает массив массивов. 
        /// Работа метода происходит не через Split, чтобы избежать потери информации, так как данные в таблице заключены в кавычках
        /// </summary>
        public static string[][] SortOfInformation(string[] massivStrings)
        {
            int kolPust = 0; // количество пустых строк
            // этот цикл нужен в случае пробелов в конце строки. однако я не пользуюсь им, так как
            // записываю строки подряд \n. но он у меня есть в случае другой задачи
            // в этой я считаю, что наличие символа НЕ ";" в конце строки - являяется исключением
            for (int i = 0; i < massivStrings.Length; i++)
            {
                if (massivStrings[i].Length == 0)
                {
                    kolPust++;
                }
            }

            // имеем 26 выборок
            string[][] massivMassivsStrings = new string[massivStrings.Length - kolPust][]; // пустые не учитываем
            int j = 0; // индекс для массива выше
            for (int i = 0; i < massivStrings.Length; i++)
            {
                if (massivStrings[i].Length != 0) // пустые строки не добавляем
                {
                    massivMassivsStrings[j] = ReadString(massivStrings[i]);
                    j++;
                }
            }
            return massivMassivsStrings;
        }
        /// <summary>
        /// Делит строку на массив данных выборок
        /// </summary>
        /// <param name="someString">Переданная строка из файла</param>
        /// <returns></returns>
        public static string[] ReadString(string someString)
        {
            // этот цикл нужен в случае пробелов в конце строки. однако я не пользуюсь им, так как
            // записываю строки подряд \n. но он у меня есть в случае другой задачи
            // в этой я считаю, что наличие символа НЕ ";" в конце строки - являяется исключением
            /* if (someString[^1] != ';')
            {
                while (someString[^1] != ';')
                {
                    someString = someString[..^1];
                }
            } */

            string[] resultOfMassivString = new string[26]; // так как всего в таблице 16 выборок
            int i = 0; // индекс для прохода по строке
            int kolProverka = 0; // индекс для заноски выборок в массив
            StringBuilder part = new StringBuilder(""); // с помощью этой переменной будем составлять каждую выборку
            while (i < someString.Length)
            {
                // если идет ячейка с =", то мы переходим к шагу с кавычками
                if ((someString[i] == '=') && (someString[i + 1] == '"'))
                {
                    i++;
                }
                part = new StringBuilder("");
                if (someString[i] == '\"')
                {
                    i++;
                    // цикл для сборки одной из выборок
                    while (someString[i] != '\"' || someString[i + 1] != ';')
                    {
                        part.Append(someString[i]);
                        i++;
                    }
                    i += 2; // сдвигаемся на после ';'
                    resultOfMassivString[kolProverka] = part.ToString();
                    kolProverka++;
                    continue;
                }
                // берем выборку, если она идет без кавычек
                while (someString[i] != ';')
                {
                    part.Append(someString[i]);
                    i++;
                }
                resultOfMassivString[kolProverka] = part.ToString(); // заносим получанную выборку в масив
                kolProverka++; // занесли выборку
                i++; // сдвигаемся с ";"
                continue;
            }
            return resultOfMassivString; // возвращаем получанный массив
        }
        
        /// <summary>
        /// Метод для записи данных в файл
        /// </summary>
        /// <param name="newMassiv"></param>
        public static bool WriteFile(string[] newMassiv, string var)
        {
            Console.WriteLine("Введите имя файла: ");
            while (true)
            {
                string name = Console.ReadLine();
                if (name == null || name.Length == 0) // Если пользователь не ввел имя, то запускаем цикл заново.
                {
                    Console.WriteLine("Вы не ввели название файла, пожалуйста, повторите ввод:");
                    continue;
                }
                CsvProcessing.fPath = name + ".csv"; // создание пути для нового файла или поиска старого

                if (var == "1")
                {
                    // если выполнялся пункт 1, то проверяем, чтобы файл был именно новый
                    if (File.Exists(CsvProcessing.fPath))
                    {
                        Console.WriteLine("Такой файл уже существует, пожалуйста, введите другое название файла!");
                        return false; // тогда пользователь вынужден выбрать другой пункт
                    }
                }

                if (var == "2")
                {
                    // если выполнялся пункт 2, то проверяем, чтобы файл был именно новый
                    if (!File.Exists(CsvProcessing.fPath))
                    {
                        Console.WriteLine("Такого файла не существует, пожалуйста, введите название файла, который уже сущетвует!");
                        return false; // тогда пользователь вынужден выбрать другой пункт
                    }
                }

                try
                {
                    // создание файла и заполнение его данными
                    using (StreamWriter sw = File.CreateText(CsvProcessing.fPath))
                    {
                        for (int i = 0; i < newMassiv.Length; i++)
                        {
                            sw.Write(newMassiv[i]);
                        }
                    }
                    Console.WriteLine("Данные записаны успешно!");
                    return true;
                }
                catch (IOException ex) // поимка одного из исключений
                {
                    Console.WriteLine("Введено некорректное название файла. Повторите попытку:");
                    continue;
                }
                catch (Exception ex) // поимка остальных
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку:");
                    continue;
                }
            }
        }
        /// <summary>
        /// Перегрузка метода выше, данные дозаписываются в файл
        /// </summary>
        /// <param name="newString">полученная строка</param>
        public static bool WriteFile(string[] newMassiv)
        {
            Console.WriteLine("Введите имя файла: ");
            while (true)
            {
                string name = Console.ReadLine();
                if (name == null || name.Length == 0) // Если пользователь не ввел имя, то запускаем цикл заново.
                {
                    Console.WriteLine("Вы не ввели название файла, пожалуйста, повторите ввод:");
                    continue;
                }
                CsvProcessing.fPath = name + ".csv"; // создание пути для нового файла или поиска старого

                // если выполнялся пункт 3, то проверяем, чтобы файл уже существовал
                if (!File.Exists(CsvProcessing.fPath))
                {
                    Console.WriteLine("Такого файла не существует, пожалуйста, введите название файла, который уже сущетвует!");
                    return false; // тогда пользователь вынужден выбрать другой пункт
                }

                try
                {
                    // создание файла и заполнение его данными
                    using (StreamWriter sw = new StreamWriter(CsvProcessing.fPath, true))
                    {
                        for (int i = 0; i < newMassiv.Length; i++)
                        {
                            sw.Write(newMassiv[i]);
                        }
                    }
                    Console.WriteLine("Данные записаны успешно!");
                    return true;
                }
                catch (IOException ex) // поимка одного из исключений
                {
                    Console.WriteLine("Введено некорректное название файла. Повторите попытку:");
                    continue;
                }
                catch (Exception ex) // поимка остальных
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку:");
                    continue;
                }
                
            }
        }
    }
}