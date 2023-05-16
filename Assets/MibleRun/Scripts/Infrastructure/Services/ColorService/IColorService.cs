using UnityEngine;

namespace Scripts.Infrastructure.Services.ColorService
{

    public interface IColorService
    {
        Color GetColorFor(int id);
    }

}