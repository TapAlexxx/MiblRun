using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public class ColorService : IColorService
    {
        public Color GetColorFor(int id)
        {
            switch (id)
            {
                case 0:
                    return new Color(76/256f, 187/256f, 241/256f);
                case 1:
                    return new Color(255/256f, 163/256f, 0/256f);
                case 2:
                    return new Color(255/256f, 179/256f, 197/256f);
                case 3:
                    return new Color(244/256f, 255/256f, 71/256f);
                case 4:
                    return new Color(184/256f, 255/256f, 194/256f);
                case 5:
                    return new Color(4/256f, 12/256f, 46/256f);
                case 6:
                    return new Color(224/256f, 1/256f, 21/256f);
                default:
                    return Color.black;
            }
        }
    }

    public interface IColorService
    {
        Color GetColorFor(int id);
    }
}