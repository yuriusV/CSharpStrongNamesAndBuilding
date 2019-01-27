namespace StudentProgram {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Common;
	using StudentLib;
	using StudentLib.Logic;


	public class Program {
		public static void Main() {
			var calc = new CalculationUtil();
			var grants = calc.CalculateGrantsForStudent(calc.Db.Students.First(), 1);
			var years = calc.GetYearsWorked(calc.Db.Teachers.First());
			Console.WriteLine("Grants: {0}, years: {1}", grants, years);
			Console.ReadKey(true);
		}
	}

}

