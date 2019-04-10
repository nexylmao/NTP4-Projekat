namespace skolski_sistem_server
{
    public static class SqlQueries
    {
        public static string CreateSkola =>
            @"IF NOT EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Skola')
                CREATE TABLE SKOLA(
                    id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
                    naziv NVARCHAR(255) NOT NULL,
                    adresa NVARCHAR(255) NOT NULL,
                    telefon NVARCHAR(15) NOT NULL,
                    email NVARCHAR(50) NOT NULL
                );";

        public static string SelectSkola =>
            @"SELECT * FROM SKOLA;";

        public static string InsertSkola =>
            @"INSERT INTO SKOLA(naziv, adresa, telefon, email) VALUES(@naziv, @adresa, @telefon, @email);";

        public static string CreateSmer =>
            @"IF NOT EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Smer')
                CREATE TABLE SMER(
                    id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
                    naziv NVARCHAR(255) NOT NULL
                );";

        public static string SelectSmer =>
            @"SELECT * FROM SMER;";

        public static string InsertSmer =>
            @"INSERT INTO SMER(naziv) VALUES(@naziv);";

        public static string CreateUcenik =>
            @"IF NOT EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Ucenik')
                CREATE TABLE UCENIK(
                    id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
                    ime NVARCHAR(30) NOT NULL,
                    prezime NVARCHAR(30) NOT NULL,
                    jmbg NVARCHAR(13) NOT NULL,
                    datumRodjenja DATE NOT NULL,
                    adresa NVARCHAR(255) NOT NULL,
                    mobilniTelefon NVARCHAR(15) NOT NULL,
                    idSmera INT FOREIGN KEY REFERENCES SMER(id),
                    idSkole INT FOREIGN KEY REFERENCES SKOLA(id)
                );";

        public static string SelectUcenik =>
            @"SELECT * FROM Ucenik u, Skola sk, Smer sm
                WHERE u.idSkole = sk.id AND u.idSmera = sm.id;";

        public static string InsertUcenik =>
            @"INSERT INTO UCENIK(ime, prezime, jmbg, datumRodjenja, adresa, mobilniTelefon, idSmera, idSkole) VALUES
            (@ime, @prezime, @jmbg, @datumRodjenja, @adresa, @mobilniTelefon, @idSmera, @idSkole);";
    }
}