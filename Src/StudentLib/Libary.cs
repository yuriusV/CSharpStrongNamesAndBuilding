namespace StudentLib {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Common;

	public abstract class Person: IIdObject, INamedObject {
		public int Age { get; set; }
		public int Id { get;set; }
		public string Name {get;set;}
	}

	public class Group: INamedObject {
		public string Name {get;set;}
		public int YearStart { get; set; }
		public Speciality Speciality {get;set;}
	}

	public class Student: Person {
		public int Grants { get; set; }
		public Group Group { get; set; }
		public bool IsContract { get; set; }
	}

	public class Teacher: Person {
		public IEnumerable<Discipline> Disciplines { get; set; }
	}

	public class TeacherWork {
		public Teacher Teacher { get; set; }
		public Group Group { get; set; }
		public int Semestr { get; set; }
		public int Year { get; set; }
	}

	public class Discipline: INamedObject {
		public string Name {get;set;}
	}

	public class Speciality: INamedObject {
		public string Name {get;set;}
		public IEnumerable<DisciplineInProgram> LearnProgram { get; set; }
	}

	public class DisciplineInProgram {
		public Discipline Discipline { get; set; }
		public int Semestr { get; set; }
		public Speciality Speciality {get;set;}
	}

	public class StudyResults {
		public Student Student { get; set; }
		public int Score { get; set; }
		public DisciplineInProgram Discipline { get; set; }
	}

}

