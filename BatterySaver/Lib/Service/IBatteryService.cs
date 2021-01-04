namespace BatterySaver.Lib.Service
{
   public interface IBatteryService
   {
      /// <summary>
      /// Gets the value indicating whether or not the system is running on battery
      /// </summary>
      bool OnBattery { get; }

      /// <summary>
      /// Gets the value indicating whether or not the system is running on AC
      /// </summary>
      bool OnAcPower { get; }

      /// <summary>
      /// Gets a value indicating whether the batter/power subsystem is in a valid/known state.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if in valid state; otherwise, <c>false</c>.
      /// </value>
      bool IsValidState { get; }

      /// <summary>
      /// Gets the detailed system power status
      /// </summary>
      /// <returns>A <see cref="SystemPowerStatus"/></returns>
      SystemPowerStatus GetSystemPowerStatus();
   }
}