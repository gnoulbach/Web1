using System;
namespace OnBuoi1.Helper
{
    public static class DateServices
    {
        // ToDo: Need to provide culturaly neutral versions.

        public static DateTime GetStartOfWeek(this DateTime dt)
        {
            DateTime ndt = dt.Subtract(TimeSpan.FromDays((int)dt.DayOfWeek));
            return new DateTime(ndt.Year, ndt.Month, ndt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfWeek(this DateTime dt)
        {
            DateTime ndt = dt.GetStartOfWeek().AddDays(6);
            return new DateTime(ndt.Year, ndt.Month, ndt.Day, 23, 59, 59, 999);
        }

        public static DateTime GetStartOfWeek(this DateTime dt, int year, int week)
        {
            DateTime dayInWeek = new DateTime(year, 1, 1).AddDays((week - 1) * 7);
            return dayInWeek.GetStartOfWeek();
        }

        public static DateTime GetEndOfWeek(this DateTime dt, int year, int week)
        {
            DateTime dayInWeek = new DateTime(year, 1, 1).AddDays((week - 1) * 7);
            return dayInWeek.GetEndOfWeek();
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtDate">Ngày nhập vào</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng được truyền vào 
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth">Thứ tự của tháng trong năm</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(int iMonth, int year)
        {
            DateTime dtResult = new DateTime(year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtInput">Ngày nhập vào</param>
        /// <returns>Ngày cuối cùng trong tháng</returns>
        public static DateTime GetLastDayOfMonth(DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng được truyền vào
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(int iMonth, int year)
        {
            DateTime dtResult = new DateTime(year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        /// <summary>
        /// So sánh ngày selected với start và end. Nếu thuộc thì trả về true, không thuộc trả về false
        /// </summary>
        /// <param name="SelectDate">Ngày được so sánh</param>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns></returns>
        public static bool CompareDate(DateTime SelectDate, DateTime startDate, DateTime endDate)
        {
            DateTime d1 = ResultStartDay(startDate);
            DateTime d2 = ResultEndDay(endDate);

            if (SelectDate >= d1 & SelectDate <= d2)
                return true;
            return false;
        }
        public static DateTime ResultStartDay(DateTime selectDate)
        {
            DateTime d1 = new DateTime(selectDate.Year, selectDate.Month, selectDate.Day, 0, 0, 0);
            return d1;
        }
        public static DateTime ResultEndDay(DateTime selectDate)
        {
            DateTime d1 = new DateTime(selectDate.Year, selectDate.Month, selectDate.Day, 23, 59, 59);
            return d1;
        }
    }
}
