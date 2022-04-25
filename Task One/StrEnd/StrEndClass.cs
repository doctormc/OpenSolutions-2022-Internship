using System;

namespace StrEnd
{
    public class StrEndClass
    {
        public static bool StrEnd(string first, string second)
        {
            //Upd 20.03.22: сорри, просто изначально работал не в репозитории, а в отдельном проекте, а сюда копировал копипастом. 

            //Очищаем от символов табуляции, пробела и новой строки 
            //2) строки могут начинаться, заканчиваться, содержать пробелы, символы табуляции, переноса строки,
            //однако подобные символы в начале и конце строки - отбрасываем
            char[] charsToTrim = { '\t','\n',' ' };
            first = first.Trim(charsToTrim);
            second = second.Trim(charsToTrim);
            
            //постарался сделать чисто алгоритмически, без сахара,можно конечно и лишние символы отбросить
            //алгоритмически но неизвестно нужно ли это. Если нужно, прошу сказать
            for (int i = 1; i <= second.Length; i++)
            {
                if(second[second.Length - i]!= first[first.Length - i])
                return false;
            }

            return true;
        }
    }
}
