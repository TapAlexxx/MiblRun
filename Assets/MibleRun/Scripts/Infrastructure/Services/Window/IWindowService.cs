using Scripts.Window;

namespace Scripts.Infrastructure.Services.Window
{
  public interface IWindowService
  {
    void Open(WindowTypeId windowTypeId);
    void CloseOpened();
  }
}