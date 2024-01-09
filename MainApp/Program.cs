using Library;
namespace MainApp
{
    public enum Function
    {
        nOfTheFirst = 1,  // 1
        nOfTheLast = 2,  // 2
    }
    internal class Program
    {
        // Максимов Тимофей Степанович, БПИ236-1, Вариант: 8
        // Файл есть в папке и имеет относительный путь ..\KDZ-4\Диспансеры версия.csv
        // C:\Users\lenovo\source\repos\KDZ-4\Диспансеры версия.csv
        // На всякий случай добавил пустые конструкторы и проверку на null
        // В экзшники уже лежат csv файлы для тестирования с названием "me" и "newfile"
        /// <summary>
        /// Метод для выбора записей из файла
        /// </summary>
        /// <param name="allInformation"></param>
        /// <param name="massivData"></param>
        /// <param name="slaceMassivData"></param>
        /// <param name="slaceAllInformation"></param>
        public static void ChooseRecords(Dispensary[] allInformation, string[][] massivData, ref string[][] slaceMassivData, ref Dispensary[] slaceAllInformation)
        {
            int N; int forFunction;
            Console.WriteLine("Введите количество записей, которые надо предоставить для просмотра:");
            while (!(int.TryParse(Console.ReadLine(), out N) && N > 1 && N <= allInformation.Length))
            {
                Console.WriteLine("Вы ввели некорректное значение либо такого количества записей в файле нет. Повторите ввод: ");
            }

            bool flag1 = true;
            while (flag1)
            {
                Console.WriteLine("Определите с какими конкретно записями будет вестись работа:");
                Console.WriteLine($"\t1. С {N} первыми");
                Console.WriteLine($"\t2. С {N} последними");
                while (!(int.TryParse(Console.ReadLine(), out forFunction) && forFunction >= 1 && forFunction <= 2))
                {
                    Console.WriteLine("Вы ввели некорректное значение. Повторите ввод: ");
                }
                Function var = (Function)forFunction;
                switch (var)
                {
                    case Function.nOfTheFirst:
                        slaceMassivData = massivData[1..][..N];
                        slaceAllInformation = allInformation[..N];
                        flag1 = false;
                        break;
                    case Function.nOfTheLast:
                        slaceMassivData = massivData[1..][^N..];
                        slaceAllInformation = allInformation[^N..];
                        flag1 = false;
                        break;
                    default:
                        Console.WriteLine("Введенное значение может быть от 1 до 2, как выбор пункта для запуска действия, повторите попытку.");
                        break;
                }
            }
        }
        static void Main()
        {
            Console.Write("Введите абсолютный путь к файлу, у которого разделитель ';': ");
            string[] result;
            while (true)
            {
                try
                {
                    CsvProcessing.FPath = Console.ReadLine(); // назначение пути
                    result = CsvProcessing.Read(); // массив строк
                    Console.WriteLine("Файл успешно считан.");
                    break;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Файл отсутствует или его структура не соответствуют варианту. Повторите попытку: ");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Возникла ошибка при открытии файла, повторите попытку: ");
                }
                catch (IOException e)
                {
                    Console.WriteLine("Введено некорректное название файла, повторите попытку: ");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите попытку: ");
                }
            }

            string[][] massivData; // массив массивов с элементами по каждой строке
            try
            {
                massivData = CsvProcessing.SortOfInformation(result);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Данные в файлe записаны неверно, начните программу заново и выберите другой файл.");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("Непредвиденная ошибка, пожалуйста, начните программу заново и выберите другой файл.");
                return;
            }
            
            // длина на 1 меньше, так как первую строку мы не учитываем
            Dispensary[] allInformation = new Dispensary[massivData.Length - 1];
            try
            {
                Methods.CreatingDispensaryMassiv(allInformation, massivData);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Данные в файле записаны неверно.");
                Console.WriteLine("Проверьте, пожалуйста, чтобы адрес, а именно улица и дом в нем были записаны через запятую.");
                Console.WriteLine("Иначе начните программу заново и выберете другой файл.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Непредвиденная ошибка, пожалуйста, начните программу заново и выберите другой файл.");
                return;
            }

            string[][] slaceMassivData = massivData[1..]; // массивы для определенного количетсва записей
            Dispensary[] slaceAllInformation = allInformation;
            bool mainFlag = true;
            ChooseRecords(allInformation, massivData, ref slaceMassivData, ref slaceAllInformation);
            do
            {
                Console.WriteLine("Укажите номер пункта меню для запуска действия:");
                Console.WriteLine("\t1. Произвести фильтрацию по полю Specialization");
                Console.WriteLine("\t2. Произвести фильтрацию по полю ChiefPosition");
                Console.WriteLine("\t3. Отсортировать таблицу по полю District");
                Console.WriteLine("\t4. Изменить выбор записей");
                Console.WriteLine("\t5. Выйти из программы");
                Console.WriteLine("\t6. Выбрать другой файл");
                string numberOfPoint = Console.ReadLine();
                switch (numberOfPoint)
                {
                    case "1":
                        Methods.SpecializationMethod(slaceAllInformation, slaceMassivData, massivData[0]);
                        break;
                    case "2":
                        Methods.ChiefPositionMethod(slaceAllInformation, slaceMassivData, massivData[0]);
                        break;
                    case "3":
                        Methods.DistrictMethod(slaceAllInformation, slaceMassivData, massivData[0]);
                        break;
                    case "4":
                        ChooseRecords(allInformation, massivData, ref slaceMassivData, ref slaceAllInformation);
                        break;
                    case "5":
                        mainFlag = false;
                        break;
                    case "6":
                        Main();
                        return;
                    default:
                        Console.WriteLine("Введенное значение может быть от 1 до 5, как выбор пункта для запуска действия, повторите попытку.");
                        break;
                }
            } while (mainFlag);
        }
    }
}