namespace Common {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IIdObject {
		int Id { get; set; }
	}

	public interface INamedObject {
		string Name { get; set; }
	}

	public interface IDataGenerator<T> {
		List<T> GenerateData();
	}
}