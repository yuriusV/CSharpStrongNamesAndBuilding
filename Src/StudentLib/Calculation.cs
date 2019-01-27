using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyVersion("1.0.0.0")]


namespace StudentLib.Logic {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Common;

	public class Db {
		public List<Group> Groups { get; set; }
		public List<Student> Students {get;set;}
		public List<Teacher> Teachers {get;set;}
		public List<TeacherWork> TeacherWorks { get; set; }
		public List<Discipline> Disciplines {get;set;}
		public List<Speciality> Specialities {get;set;}
		public List<DisciplineInProgram> DisciplineInPrograms {get;set;}
		public List<StudyResults> StudyResults {get;set;}

		public Db() {
			Groups = new List<Group>();
			Students = new List<Student>();
			Teachers = new List<Teacher>();
			TeacherWorks = new List<TeacherWork>();
			Disciplines = new List<Discipline>();
			Specialities = new List<Speciality>();
			DisciplineInPrograms = new List<DisciplineInProgram>();
			StudyResults = new List<StudyResults>();
		}

		public void Load() {
			var group2 = new Group() {Name = "IP-52", YearStart = 2015};
			var group1 = new Group() {Name = "IP-51", YearStart = 2015};

			var student1 = new Student() { Group = group2, IsContract = false, Name = "Yura" };
			var student2 = new Student() { Group = group2, IsContract = true, Name = "Petya" };
			var student3 = new Student() { Group = group1, IsContract = false, Name = "Ivan" };

			var discipline1 = new Discipline() { Name = "Math" };
			var discipline2 = new Discipline() { Name = "History" };

			var teacher1 = new Teacher() { Name = "Teacher1", Disciplines = new List<Discipline>() {discipline1}};
			var teacher2 = new Teacher() { Name = "Teacher2", Disciplines = new List<Discipline>() {discipline2}};

			

			var tWork1 = new TeacherWork() { Teacher = teacher1, Group = group1, Semestr = 1, Year = 2015 };
			var tWork2 = new TeacherWork() { Teacher = teacher1, Group = group1, Semestr = 2, Year = 2016 };
			var tWork3 = new TeacherWork() { Teacher = teacher2, Group = group2, Semestr = 1, Year = 2015 };

			var discProgram1 = new DisciplineInProgram() { Discipline = discipline1, Semestr = 1 };
			var discProgram2 = new DisciplineInProgram() { Discipline = discipline2, Semestr = 2 };
			var discProgram3 = new DisciplineInProgram() { Discipline = discipline1, Semestr = 1 };

			var speciality1 = new Speciality() {
				Name = "PI", 
				LearnProgram = new List<DisciplineInProgram>() { discProgram1, discProgram2 } 
			};


			var studyRes1 = new StudyResults() { Student = student1, Score = 94, Discipline = discProgram1 };
			var studyRes2 = new StudyResults() { Student = student1, Score = 81, Discipline = discProgram2 };
			var studyRes3 = new StudyResults() { Student = student2, Score = 74, Discipline = discProgram3 };


			Groups.Add(group1); Groups.Add(group2);
			Students.Add(student1); Students.Add(student2); Students.Add(student3);
			Teachers.Add(teacher1); Teachers.Add(teacher2);
			Disciplines.Add(discipline1); Disciplines.Add(discipline1);
			TeacherWorks.Add(tWork1); TeacherWorks.Add(tWork2); TeacherWorks.Add(tWork3);
			DisciplineInPrograms.Add(discProgram1); DisciplineInPrograms.Add(discProgram2); DisciplineInPrograms.Add(discProgram3);
			Specialities.Add(speciality1);
			StudyResults.Add(studyRes1); StudyResults.Add(studyRes2); StudyResults.Add(studyRes3);

		}
	}

	public class CalculationUtil {
		public Db Db;
		protected FacultyInfo _facultyInfo;

		public CalculationUtil () {
			Db = new Db();
			Db.Load();
			_facultyInfo = new FacultyInfo();
		}

		public decimal CalculateGrantsForStudent(Student student, int semestr) {
			if (student.IsContract) {
				return -_facultyInfo.GetContractPayment();
			}

			var results = Db.StudyResults;
			var semestrResults = results.Where(x => x.Discipline.Semestr == semestr && x.Student == student);
			var totalCount = semestrResults.Count();
			var countThrees = semestrResults.Count(x => x.Score < 75);
			var countFives = semestrResults.Count(x => x.Score >= 95);

			if (countFives >= countThrees && countFives < totalCount) {
				return _facultyInfo.GetBaseGrants();
			} else if (countFives == totalCount) {
				return _facultyInfo.GetAdvancedGrants();
			} else {
				return 0m;
			}
		}

		public int GetYearsWorked(Teacher teacher) {
			var works = Db.TeacherWorks;
			var currentYear = DateTime.Now.Year;

			var teacherWorks = works.Where(x => x.Teacher == teacher).Select(x => x.Year); 
			return (int)(teacherWorks.Max() 
				- teacherWorks.Min());
		}
	}	

	public class FacultyInfo {
		public decimal GetBaseGrants() {
			return 1000m;
		}

		public decimal GetAdvancedGrants() {
			return 1500m;
		}

		public decimal GetContractPayment() {
			return 15000m;
		}
	}
}