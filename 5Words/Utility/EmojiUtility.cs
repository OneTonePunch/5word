using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Utility
{
    public static class EmojiUtility
    {
        private static Dictionary<EmojiType, Int32> Emojis = new Dictionary<EmojiType, Int32> {
            { EmojiType.Grining, 0X1F601 },
            { EmojiType.Winking, 0X1F609 },
            { EmojiType.Disappointed, 0X1F625},
            { EmojiType.Fearful, 0X1F628},
            { EmojiType.Weary, 0X1F640},
        };

        public static string GetEmojiChar(EmojiType type)
        {
            var value = Emojis.Where(x => x.Key == type).FirstOrDefault().Value;
            return char.ConvertFromUtf32(value);
        }
    }
    public enum EmojiType
    {
        Grining, //0X1F601 улыбка
        Winking, //0X1F609 подмигивание
        Disappointed, //0X1F625 грусная слеза
        Fearful, //0X1F628 испуганный
        Weary, // 0X1F640 удивленный кот
    }
}
