using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class Methods
    {
        /// <summary>
        /// Поиск максимальной длины элемента для каждой выборки, чтобы позже выводить данные в читаемом табличном виде
        /// </summary>
        /// <param name="massivData">массив массивов</param>
        /// <returns></returns>
        public static int[] MaxLen(string[][] massivData)
        {
            int[] maxMassiv = new int[massivData[0].Length];
            for (int i = 0; i < massivData[0].Length; i++)
            {
                for (int j = 0; j < massivData.Length; j++)
                {
                    if (maxMassiv[i] < massivData[j][i].Length)
                    {
                        maxMassiv[i] = massivData[j][i].Length;
                    }
                }
            }
            return maxMassiv;
        }
        /// <summary>
        /// Поиск индекса с нужной выборкой
        /// </summary>
        /// <param name="massivData"></param>
        /// <param name="var"></param>
        public static int Search(string[][] massivData, string var)
        {
            int indexArea = -1;
            // ищу номер элемента с такой выборкой
            for (int i = 0; i < massivData[0].Length; i++)
            {
                if (massivData[0][i] == var)
                {
                    indexArea = i;
                    break;
                }
            }
            if (indexArea == -1)
            {
                Console.WriteLine("Ваш файл не соотвествует заявленным выборкам. Перезаргузите команду и выберите другой.");
                indexArea = 0;
            }
            return indexArea;
        }
        /// <summary>
        /// Вывод строки на экран
        /// </summary>
        public static void PrintString(string[][] massivData, int[] maxlen, int i)
        {
            for (int j = 0; j < massivData[i].Length; j++) // цикл для вывода массива
            {
                Console.Write($"{massivData[i][j]}");
                // это цикл для пробелов, если в ячейке еще осталось место после вывода элемента
                for (int f = 0; f < maxlen[j] - massivData[i][j].Length + 1; f++)
                // +1 пробел так как хотя бы какой-то разделитель между элементами должен быть
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Метод переводит массив элементов строки обратно в строку
        /// </summary>
        /// <param name="someString">Переданный массив элементов</param>
        public static string MyToString(string[] someString)
        {
            StringBuilder stringNew = new StringBuilder("");
            foreach (string el in someString)
            {
                stringNew.Append($"\"{el}\";");
            }
            stringNew.Append("\n");
            return stringNew.ToString();
        }
        /// <summary>
        /// Метод для фильтрации по выборке specialization
        /// </summary>
        /// <param name="allInformation"></param>
        /// <param name="massivData"></param>
        public static void SpecializationMethod(Dispensary[] allInformation, string[][] massivData, string[] firstString)
        {
            Console.Write("Введите значение для данной выборки: ");
            string specialization = Console.ReadLine();
            while (specialization == null || specialization.Length == 0)
            {
                Console.Write("Введите значение не null и не пустое: ");
                specialization = Console.ReadLine();
            }

            int[] maxlen = MaxLen(massivData);

            int kol = 0; // переменная для посчета строк, подходящих под выборку
            for (int i = 0; i < allInformation.Length; i++)
            {
                if (allInformation[i].Specialization.Contains(specialization))
                {

                    PrintString(massivData, maxlen, i);
                    kol++;
                }
            }

            // массив и индекс для его заполнения
            int indexSort; string[] sortMassiv;
            indexSort = 1;
            sortMassiv = new string[kol + 1];
            sortMassiv[0] = MyToString(firstString);
            for (int i = 0; i < allInformation.Length; i++)
            {
                if (allInformation[i].Specialization.Contains(specialization))
                {
                    sortMassiv[indexSort] = MyToString(massivData[i]);
                    indexSort++;
                }
            }

            if (sortMassiv.Length == 1)
            {
                Console.WriteLine("К сожалению, таких данных в базе нет :(");
            }
            else
            {
                ChooseWriting(sortMassiv);
            }
        }
        /// <summary>
        /// Метод для фильтрации по выборке chiefPosition
        /// </summary>
        /// <param name="allInformation"></param>
        /// <param name="massivData"></param>
        public static void ChiefPositionMethod(Dispensary[] allInformation, string[][] massivData, string[] firstString)
        {
            Console.Write("Введите значение для данной выборки: ");
            string chiefPosition = Console.ReadLine();
            while (chiefPosition == null || chiefPosition.Length == 0)
            {
                Console.Write("Введите значение не null и не пустое: ");
                chiefPosition = Console.ReadLine();
            }

            int[] maxlen = MaxLen(massivData);

            int kol = 0; // переменная для посчета строк, подходящих под выборку
            for (int i = 0; i < allInformation.Length; i++)
            {
                if (allInformation[i].ChiefPosition.Contains(chiefPosition))
                {

                    PrintString(massivData, maxlen, i);
                    kol++;
                }
            }

            // массив и индекс для его заполнения
            int indexSort; string[] sortMassiv;
            indexSort = 1;
            sortMassiv = new string[kol + 1];
            sortMassiv[0] = MyToString(firstString);
            for (int i = 0; i < allInformation.Length; i++)
            {
                if (allInformation[i].ChiefPosition.Contains(chiefPosition))
                {
                    sortMassiv[indexSort] = MyToString(massivData[i]);
                    indexSort++;
                }
            }

            if (sortMassiv.Length == 1)
            {
                Console.WriteLine("К сожалению, таких данных в базе нет :(");
            }
            else
            {
                ChooseWriting(sortMassiv);
            }
        }
        /// <summary>
        /// Метод для сортировки поля district
        /// </summary>
        /// <param name="allInformation"></param>
        /// <param name="massivData"></param>
        /// <param name="firstString"></param>
        public static void DistrictMethod(Dispensary[] allInformation, string[][] massivData, string[] firstString)
        {
            if (String.Compare(allInformation[0].AdmArea, allInformation[1].AdmArea) > 0)
            {
                // то они расположены в обратном порядке алфавита
                Console.WriteLine("Административные округа расположены в обратном алфавитном порядке, поэтому сортировка для district тоже будет в обратном алфавитном порядке.");
                for (int i = allInformation.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (String.Compare(allInformation[j].District, allInformation[j + 1].District) < 0)
                        {
                            string[] tmp = massivData[j][..]; // [..] для копирование значений, а не ссылок
                            massivData[j] = massivData[j+1][..];
                            massivData[j+1] = tmp[..];

                            Dispensary tmp1 = allInformation[j];
                            allInformation[j] = allInformation[j + 1];
                            allInformation[j + 1] = tmp1;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Административные округа расположены в алфавитном порядке, поэтому сортировка для district тоже будет в алфавитном порядке.");
                for (int i = allInformation.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (String.Compare(allInformation[j].District, allInformation[j+1].District) > 0)
                        {
                            string[] tmp = massivData[j][..]; // [..] для копирование значений, а не ссылок
                            massivData[j] = massivData[j+1][..];
                            massivData[j+1] = tmp[..];

                            Dispensary tmp1 = allInformation[j];
                            allInformation[j] = allInformation[j+1];
                            allInformation[j + 1] = tmp1;
                        }
                    }
                }
            }


            // массив для вывода данных на экран. в нем лежит информация о размере ячейки для каждого элемента
            // я ее ищу в методе, чтобы все данные выводились в табличном читаемом виде
            int[] maxlen = MaxLen(massivData);
            for (int i = 0; i < massivData.Length; i++)
            {
                PrintString(massivData, maxlen, i);
            }

            // запись отсорированного массива массивов в массив строк
            string[] sortMassiv = new string[massivData.Length+1];
            sortMassiv[0] = MyToString(firstString);
            for (int i = 0; i < massivData.Length; i++)
            {
                sortMassiv[i+1] = MyToString(massivData[i]);
            }

            ChooseWriting(sortMassiv);
        }
        /// <summary>
        /// Метод для заполнения массива объектов всех данных файла
        /// </summary>
        /// <param name="allInformation"></param>
        /// <param name="massivData"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void CreatingDispensaryMassiv(Dispensary[] allInformation, string[][] massivData)
        {
            for (int i = 1; i < massivData.Length;i++)
            {

                string[] addressInfo = massivData[i][6].Split(',');
                string street; string houseNumber;
                if (addressInfo.Length > 1)
                {
                    // получаю улицу
                    street = addressInfo[0].Trim(' ');
                    // получаю номер дома 
                    if (addressInfo[1].Trim(' ').Split(" ").Length > 1)
                    {
                        houseNumber = addressInfo[1].Trim(' ').Split(" ")[1];
                    }
                    else
                    {
                        houseNumber = addressInfo[1].Trim(' ').Split(" ")[0];
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
                allInformation[i-1] = new Dispensary(massivData[i][5], street, houseNumber, massivData[i][11], massivData[i][12],
                    massivData[i][0], massivData[i][1], massivData[i][2], massivData[i][3],
                    massivData[i][4], massivData[i][7], massivData[i][8], massivData[i][9], massivData[i][10],
                    massivData[i][13], massivData[i][14], massivData[i][15], massivData[i][16], massivData[i][17], massivData[i][18], massivData[i][19],
                    massivData[i][20], massivData[i][21], massivData[i][22], massivData[i][23], massivData[i][24], massivData[i][25]);
            }
        }
        public static void ChooseWriting(string[] newMassiv)
        {
            bool flag = true;
            int N;
            while (flag)
            {
                Console.WriteLine("Определите дальнейшую работу с данными (выберите пункт):");
                Console.WriteLine($"\t1. Создание нового файла");
                Console.WriteLine($"\t2. Замена содержимого уже существующего файла");
                Console.WriteLine($"\t3. Добавление сохраняемых данных к содержимому существующего файла");
                string var = Console.ReadLine();
                switch (var)
                {
                    case "1":
                        while (!CsvProcessing.WriteFile(newMassiv, var)) { }
                        flag = false;
                        break;
                    case "2":
                        while (!CsvProcessing.WriteFile(newMassiv, var)) { }
                        flag = false;
                        break;
                    case "3":
                        while (!CsvProcessing.WriteFile(newMassiv)) { }
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Введенное значение может быть от 1 до 3, как выбор пункта для запуска действия, повторите попытку.");
                        break;
                }
            }
        }
    }
}
