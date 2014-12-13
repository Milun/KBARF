#include <iostream>
#include <fstream>

using namespace std;

int main(int argc, char* argv[])
{
	// Check for faulty file read.
	if (argc != 2) {
		cout << "ERROR! Drag and drop the .obj file onto this .exe!" << endl << endl;
		system("pause");

		exit(1);
		return 0;
	}

	std::ifstream infile("thefile.txt");

	cout << "\n" << "Programm finished...\n\n" << endl;

	cout << argv[1];



	system("pause");

	// cin.ignore();
	return 0;
}