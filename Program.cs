using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
	static void Main()
	{
		// Khởi tạo danh sách các môn học
		List<string> subjects = new List<string>();

		// Nhập danh sách môn học từ người dùng
		Console.WriteLine("Enter a subject list (Press Enter after each subject, type 'end' to finish): ");
		while (true)
		{
			string subject = Console.ReadLine();
			if (subject.ToLower() == "end")
				break;
			subjects.Add(subject);
		}

		// Phân bố đều môn học vào các ngày
		Dictionary<string, (string dayOfWeek, string time)> schedule = ScheduleClasses(subjects);

		// In lịch học
		Console.WriteLine("\nYour class schedule:");
		foreach (var subject in schedule.Keys)
		{
			Console.WriteLine($"{subject}: {schedule[subject].dayOfWeek}, {schedule[subject].time}");
		}
	}

	static Dictionary<string, (string, string)> ScheduleClasses(List<string> subjects)
	{
		Dictionary<string, (string dayOfWeek, string time)> schedule = new Dictionary<string, (string, string)>();

		string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
		string[] times = { "08:00 - 10:00", "10:00 - 12:00", "13:00 - 15:00", "15:00 - 17:00" };

		int maxClassesPerDay = (int)Math.Ceiling((double)subjects.Count / daysOfWeek.Length);
		int[] classesCountPerDay = new int[daysOfWeek.Length];

		foreach (var subject in subjects)
		{
			// Tìm ngày và thời gian trống cho môn học
			string dayOfWeek = "";
			string time = "";

			bool found = false;
			foreach (var day in daysOfWeek)
			{
				foreach (var t in times)
				{
					string candidate = $"{day}, {t}";

					// Kiểm tra xem thời gian này có trùng với môn nào trong lịch hay không
					bool conflict = false;
					foreach (var scheduledSubject in schedule.Values)
					{
						if (scheduledSubject.dayOfWeek == day && scheduledSubject.time == t)
						{
							conflict = true;
							break;
						}
					}

					// Kiểm tra xem số môn trong ngày đã đạt tối đa chưa
					int dayIndex = Array.IndexOf(daysOfWeek, day);
					if (!conflict && classesCountPerDay[dayIndex] < maxClassesPerDay)
					{
						dayOfWeek = day;
						time = t;
						found = true;
						classesCountPerDay[dayIndex]++;
						break;
					}
				}
				if (found)
					break;
			}

			schedule.Add(subject, (dayOfWeek, time));
		}

		return schedule;
	}
}
