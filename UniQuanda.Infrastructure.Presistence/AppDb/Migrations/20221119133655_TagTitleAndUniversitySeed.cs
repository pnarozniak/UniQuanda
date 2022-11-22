using Microsoft.EntityFrameworkCore.Migrations;
using UniQuanda.Core.Domain.Enums;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    public partial class TagTitleAndUniversitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AcademicTitles",
                columns: new[] { "Id", "AcademicTitleType", "Name" },
                values: new object[,]
                {
                    { 1, AcademicTitleEnum.Engineer, "inż." },
                    { 2, AcademicTitleEnum.Engineer, "mgr inż." },
                    { 3, AcademicTitleEnum.Engineer, "dr inż." },
                    { 4, AcademicTitleEnum.Engineer, "dr hab. inż." },
                    { 5, AcademicTitleEnum.Engineer, "prof." },
                    { 6, AcademicTitleEnum.Bachelor, "lic." },
                    { 7, AcademicTitleEnum.Bachelor, "mgr" },
                    { 8, AcademicTitleEnum.Bachelor, "dr" },
                    { 9, AcademicTitleEnum.Bachelor, "dr hab." },
                    { 10, AcademicTitleEnum.Bachelor, "prof." },
                    { 11, AcademicTitleEnum.Academic, "prof. PJATK" },
                    { 12, AcademicTitleEnum.Academic, "prof. UŚ" },
                    { 13, AcademicTitleEnum.Academic, "prof. PW" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "ParentTagId" },
                values: new object[,]
                {
                    { 1, "Programowanie i tematy pokrewne dotyczące informatyki", null, "Informatyka", null },
                    { 2, "Zagadnienia z dziedziny matematyki", null, "Matematyka", null },
                    { 3, "Zagadnienia z dziedziny fizyki", null, "Fizyka", null },
                    { 4, "Zagadnienia z dziedziny chemii", null, "Chemia", null },
                    { 5, "Zagadnienia z dziedziny biologii", null, "Biologia", null },
                    { 6, "Zagadnienia z dziedziny geografii", null, "Geografia", null },
                    { 7, "Zagadnienia z dziedziny historii", null, "Historia", null },
                    { 8, "Zagadnienia z dziedziny filozofii", null, "Filozofia", null },
                    { 9, "Zagadnienia z dziedziny psychologii", null, "Psychologia", null },
                    { 10, "Zagadnienia z dziedziny dziedzin sztuki", null, "Sztuka", null },
                    { 11, "Zagadnienia z dziedziny języków obcych", null, "Filologia", null },
                    { 12, "Zagadnienia z dziedziny polskiego prawa", null, "Prawo", null },
                    { 13, "Zagadnienia z dziedziny międzynarodowego prawa", null, "Prawo międzynarodowe", null },
                    { 14, "Zagadnienia z dziedziny medycyny", null, "Medycyna", null },
                    { 15, "Zagadnienia z dziedziny inżynierii", null, "Inżyneria", null },
                    { 16, "Zagadnienia z dziedziny ekonomii i finansów", null, "Ekonomia", null },
                    { 17, "Nauki polityczne, nauki o polityce, nauka o polityce. Nauka społeczna zajmująca się działalnością związaną ze sprawowaniem władzy polityczne", null, "Politologia", null },
                    { 18, "Zagadnienia z dziedziny religii", null, "Teologia", null },
                    { 19, "Zagadnienia z dziedziny edukacji i wychowania", null, "Pedagogika", null },
                    { 20, "Zagadnienia z dziedziny sportu", null, "Sport", null },
                    { 21, "Zagadnienia z dziedziny turystyki i podróży", null, "Turystyka", null },
                    { 22, "Zagadnienia z dziedziny logistyki i transportu", null, "Logistyka", null },
                    { 23, "Zagadnienia z dziedziny marketingu i reklamy", null, "Marketing", null },
                    { 24, "Zagadnienia z dziedziny dziennikarstwa", null, "Dziennikarstwo", null },
                    { 25, "Zagadnienia z dziedziny architektury", null, "Architektura", null },
                    { 26, "Zagadnienia związane z lotnictwem", null, "Lotnictwo", null }
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Logo", "Name" },
                values: new object[,]
                {
                    { 1, "https://pja.edu.pl/templates/pjwstk/favicon.ico", "Polsko-Japońska Akademia Technik Komputerowych" },
                    { 2, "https://us.edu.pl/wp-content/uploads/strona-g%C5%82%C3%B3wna/favicon/cropped-favicon_navy_white-32x32.png", "Uniwersytet śląski w Katowicach" },
                    { 3, "https://www.pw.edu.pl/design/pw/images/favicon.ico", "Politechnika Warszawska" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "ParentTagId" },
                values: new object[,]
                {
                    { 27, null, "https://dev.pl:2002/api/Image/Tags/programowanie.jpg", "Programowanie", 1 },
                    { 28, null, "https://dev.pl:2002/api/Image/Tags/bazy_danych.jpg", "Bazy danych", 1 },
                    { 29, null, "https://dev.pl:2002/api/Image/Tags/sieci_komputerowe.jpg", "Sieci komputerowe", 1 },
                    { 30, null, "https://dev.pl:2002/api/Image/Tags/systemy_operacyjne.jpg", "Systemy operacyjne", 1 },
                    { 31, null, "https://dev.pl:2002/api/Image/Tags/angular.jpg", "Angular", 1 },
                    { 32, null, "https://dev.pl:2002/api/Image/Tags/asp_net.jpg", "ASP.NET", 1 },
                    { 33, null, "https://dev.pl:2002/api/Image/Tags/c_sharp.jpg", "C#", 1 },
                    { 34, null, "https://dev.pl:2002/api/Image/Tags/c_plus_plus.jpg", "C++", 1 },
                    { 35, null, "https://dev.pl:2002/api/Image/Tags/java.jpg", "Java", 1 },
                    { 36, null, "https://dev.pl:2002/api/Image/Tags/javascript.jpg", "JavaScript", 1 },
                    { 37, null, "https://dev.pl:2002/api/Image/Tags/php.jpg", "PHP", 1 },
                    { 38, null, "https://dev.pl:2002/api/Image/Tags/python.jpg", "Python", 1 },
                    { 39, null, "https://dev.pl:2002/api/Image/Tags/ruby.jpg", "Ruby", 1 },
                    { 40, null, "https://dev.pl:2002/api/Image/Tags/sql.jpg", "SQL", 1 },
                    { 41, null, "https://dev.pl:2002/api/Image/Tags/typescript.jpg", "TypeScript", 1 },
                    { 42, null, "https://dev.pl:2002/api/Image/Tags/wpf.jpg", "WPF", 1 },
                    { 43, null, "https://dev.pl:2002/api/Image/Tags/xamarin.jpg", "Xamarin", 1 },
                    { 44, null, "https://dev.pl:2002/api/Image/Tags/inzynieria_oprogramowania.jpg", "Inżynieria oprogramowania", 1 },
                    { 45, null, "https://dev.pl:2002/api/Image/Tags/architektura_oprogramowania.jpg", "Architektura oprogramowania", 1 },
                    { 46, null, "https://dev.pl:2002/api/Image/Tags/testowanie_oprogramowania.jpg", "Testowanie oprogramowania", 1 },
                    { 47, null, "https://dev.pl:2002/api/Image/Tags/sztuczna_inteligencja.jpg", "Sztuczna inteligencja", 1 },
                    { 48, null, "https://dev.pl:2002/api/Image/Tags/programowanie_obiektowe.jpg", "Programowanie obiektowe", 1 },
                    { 49, null, "https://dev.pl:2002/api/Image/Tags/programowanie_funkcyjne.jpg", "Programowanie funkcyjne", 1 },
                    { 50, null, "https://dev.pl:2002/api/Image/Tags/programowanie_proceduralne.jpg", "Programowanie proceduralne", 1 },
                    { 51, null, "https://dev.pl:2002/api/Image/Tags/programowanie_zdarzeniowe.jpg", "Programowanie zdarzeniowe", 1 },
                    { 52, null, "https://dev.pl:2002/api/Image/Tags/programowanie_asynchroniczne.jpg", "Programowanie asynchroniczne", 1 },
                    { 53, null, "https://dev.pl:2002/api/Image/Tags/programowanie_równoległe.jpg", "Programowanie równoległe", 1 },
                    { 54, null, "https://dev.pl:2002/api/Image/Tags/programowanie_wielowątkowe.jpg", "Programowanie wielowątkowe", 1 },
                    { 55, null, "https://dev.pl:2002/api/Image/Tags/programowanie_wieloprocesowe.jpg", "Programowanie wieloprocesowe", 1 },
                    { 56, null, "https://dev.pl:2002/api/Image/Tags/programowanie_wielokomputerowe.jpg", "Programowanie wielokomputerowe", 1 },
                    { 57, null, "https://dev.pl:2002/api/Image/Tags/programowanie_rozproszone.jpg", "Programowanie rozproszone", 1 },
                    { 58, null, "https://dev.pl:2002/api/Image/Tags/programowanie_rozproszone.jpg", "Programowanie rozproszone", 1 },
                    { 59, null, "https://dev.pl:2002/api/Image/Tags/programowanie_równoległe.jpg", "Programowanie równoległe", 1 },
                    { 60, null, "https://dev.pl:2002/api/Image/Tags/programowanie_wielowątkowe.jpg", "Programowanie wielowątkowe", 1 },
                    { 61, null, "https://dev.pl:2002/api/Image/Tags/matematyka_dyskretna.jpg", "Matematyka dyskretna", 2 },
                    { 62, null, "https://dev.pl:2002/api/Image/Tags/planimetria.jpg", "Planimetria", 2 },
                    { 63, null, "https://dev.pl:2002/api/Image/Tags/analiza_matematyczna.jpg", "Analiza matematyczna", 2 },
                    { 64, null, "https://dev.pl:2002/api/Image/Tags/algebra_liniowa.jpg", "Algebra liniowa", 2 },
                    { 65, null, "https://dev.pl:2002/api/Image/Tags/geometria.jpg", "Geometria", 2 },
                    { 66, null, "https://dev.pl:2002/api/Image/Tags/logika_matematyczna.jpg", "Logika matematyczna", 2 },
                    { 67, null, "https://dev.pl:2002/api/Image/Tags/teoria_liczb.jpg", "Teoria liczb", 2 },
                    { 68, null, "https://dev.pl:2002/api/Image/Tags/teoria_mnogości.jpg", "Teoria mnogości", 2 },
                    { 69, null, "https://dev.pl:2002/api/Image/Tags/teoria_grafów.jpg", "Teoria grafów", 2 },
                    { 70, null, "https://dev.pl:2002/api/Image/Tags/teoria_gier.jpg", "Teoria gier", 2 },
                    { 71, null, "https://dev.pl:2002/api/Image/Tags/teoria_kodowania.jpg", "Teoria kodowania", 2 },
                    { 72, null, "https://dev.pl:2002/api/Image/Tags/teoria_obliczeń.jpg", "Teoria obliczeń", 2 },
                    { 73, null, "https://dev.pl:2002/api/Image/Tags/teoria_algorytmów.jpg", "Teoria algorytmów", 2 },
                    { 74, null, "https://dev.pl:2002/api/Image/Tags/algebra_nieliniowa.jpg", "Algebra nieliniowa", 2 },
                    { 75, null, "https://dev.pl:2002/api/Image/Tags/staystyka.jpg", "Staystyka", 2 },
                    { 76, null, "https://dev.pl:2002/api/Image/Tags/matematyka_stosowana.jpg", "Matematyka stosowana", 2 },
                    { 77, null, "https://dev.pl:2002/api/Image/Tags/topologia.jpg", "Topologia", 2 },
                    { 78, null, "https://dev.pl:2002/api/Image/Tags/analiza_numeryczna.jpg", "Analiza numeryczna", 2 },
                    { 79, null, "https://dev.pl:2002/api/Image/Tags/analiza_funkcjonalna.jpg", "Analiza funkcjonalna", 2 },
                    { 80, null, "https://dev.pl:2002/api/Image/Tags/analiza_zespolona.jpg", "Analiza zespolona", 2 },
                    { 81, null, "https://dev.pl:2002/api/Image/Tags/analiza_fouriera.jpg", "Analiza Fouriera", 2 },
                    { 82, null, "https://dev.pl:2002/api/Image/Tags/analiza_graniczna.jpg", "Analiza graniczna", 2 },
                    { 83, null, "https://dev.pl:2002/api/Image/Tags/analiza_różnicowa.jpg", "Analiza różnicowa", 2 },
                    { 84, null, "https://dev.pl:2002/api/Image/Tags/analiza_integralna.jpg", "Analiza integralna", 2 },
                    { 85, null, "https://dev.pl:2002/api/Image/Tags/analiza_funkcji_wielu_zmiennych.jpg", "Analiza funkcji wielu zmiennych", 2 },
                    { 86, null, "https://dev.pl:2002/api/Image/Tags/analiza_funkcji_rzeczywistych.jpg", "Analiza funkcji rzeczywistych", 2 },
                    { 87, null, "https://dev.pl:2002/api/Image/Tags/analiza_funkcji_zespolonych.jpg", "Analiza funkcji zespolonych", 2 },
                    { 88, null, "https://dev.pl:2002/api/Image/Tags/fizyka_kwantowa.jpg", "Fizyka Kwantowa", 3 },
                    { 89, null, "https://dev.pl:2002/api/Image/Tags/fizyka_atomowa.jpg", "Fizyka atomowa", 3 },
                    { 90, null, "https://dev.pl:2002/api/Image/Tags/fizyka_molekularna.jpg", "Fizyka molekularna", 3 },
                    { 91, null, "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_stałego.jpg", "Fizyka ciała stałego", 3 },
                    { 92, null, "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_rzadkiego.jpg", "Fizyka ciała rzadkiego", 3 },
                    { 93, null, "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_płynnego.jpg", "Fizyka ciała płynnego", 3 },
                    { 94, null, "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_gazowego.jpg", "Fizyka ciała gazowego", 3 },
                    { 95, null, "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_promieniotwórczego.jpg", "Fizyka ciała promieniotwórczego", 3 },
                    { 96, null, "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_złożonego.jpg", "Fizyka ciała złożonego", 3 },
                    { 97, null, "https://dev.pl:2002/api/Image/Tags/chemia_organiczna.jpg", "Chemia organiczna", 4 },
                    { 98, null, "https://dev.pl:2002/api/Image/Tags/chemia_analityczna.jpg", "Chemia analityczna", 4 },
                    { 99, null, "https://dev.pl:2002/api/Image/Tags/chemia_fizyczna.jpg", "Chemia fizyczna", 4 },
                    { 100, null, "https://dev.pl:2002/api/Image/Tags/chemia_inorganiczna.jpg", "Chemia inorganiczna", 4 },
                    { 101, null, "https://dev.pl:2002/api/Image/Tags/chemia_biologiczna.jpg", "Chemia biologiczna", 4 },
                    { 102, null, "https://dev.pl:2002/api/Image/Tags/biologia_molekularna.jpg", "Biologia molekularna", 5 },
                    { 103, null, "https://dev.pl:2002/api/Image/Tags/biologia_komórkowa.jpg", "Biologia komórkowa", 5 },
                    { 104, null, "https://dev.pl:2002/api/Image/Tags/biologia_ewolucyjna.jpg", "Biologia ewolucyjna", 5 },
                    { 105, null, "https://dev.pl:2002/api/Image/Tags/biologia_systematyczna.jpg", "Biologia systematyczna", 5 },
                    { 106, null, "https://dev.pl:2002/api/Image/Tags/biologia_populacyjna.jpg", "Biologia populacyjna", 5 },
                    { 107, null, "https://dev.pl:2002/api/Image/Tags/biologia_ekologiczna.jpg", "Biologia ekologiczna", 5 },
                    { 108, null, "https://dev.pl:2002/api/Image/Tags/biologia_ekonomii.jpg", "Biologia ekonomii", 5 },
                    { 109, null, "https://dev.pl:2002/api/Image/Tags/biologia_roślin.jpg", "Biologia roślin", 5 },
                    { 110, null, "https://dev.pl:2002/api/Image/Tags/biologia_zwierząt.jpg", "Biologia zwierząt", 5 },
                    { 111, null, "https://dev.pl:2002/api/Image/Tags/geomorfologia.jpg", "Geomorfologia", 6 },
                    { 112, null, "https://dev.pl:2002/api/Image/Tags/geologia.jpg", "Geologia", 6 },
                    { 113, null, "https://dev.pl:2002/api/Image/Tags/geografia_fizyczna.jpg", "Geografia fizyczna", 6 },
                    { 114, null, "https://dev.pl:2002/api/Image/Tags/geografia_polityczna.jpg", "Geografia polityczna", 6 },
                    { 115, null, "https://dev.pl:2002/api/Image/Tags/geografia_ekonomiczna.jpg", "Geografia ekonomiczna", 6 },
                    { 116, null, "https://dev.pl:2002/api/Image/Tags/geografia_społeczna.jpg", "Geografia społeczna", 6 },
                    { 117, null, "https://dev.pl:2002/api/Image/Tags/geografia_kulturowa.jpg", "Geografia kulturowa", 6 },
                    { 118, null, "https://dev.pl:2002/api/Image/Tags/geografia_regionalna.jpg", "Geografia regionalna", 6 },
                    { 119, null, "https://dev.pl:2002/api/Image/Tags/geografia_turystyki.jpg", "Geografia turystyki", 6 },
                    { 120, null, "https://dev.pl:2002/api/Image/Tags/geografia_fizyczna.jpg", "Geografia fizyczna", 6 },
                    { 121, null, "https://dev.pl:2002/api/Image/Tags/historia_starożytna.jpg", "Historia starożytna", 7 },
                    { 122, null, "https://dev.pl:2002/api/Image/Tags/historia_średniowiecza.jpg", "Historia średniowiecza", 7 },
                    { 123, null, "https://dev.pl:2002/api/Image/Tags/historia_nowożytna.jpg", "Historia nowożytna", 7 },
                    { 124, null, "https://dev.pl:2002/api/Image/Tags/historia_współczesna.jpg", "Historia współczesna", 7 },
                    { 125, null, "https://dev.pl:2002/api/Image/Tags/filozofia.jpg", "Filozofia", 8 },
                    { 126, null, "https://dev.pl:2002/api/Image/Tags/nauki_społeczne.jpg", "Nauki społeczne", 8 },
                    { 127, null, "https://dev.pl:2002/api/Image/Tags/socjologia.jpg", "Socjologia", 8 },
                    { 128, null, "https://dev.pl:2002/api/Image/Tags/antropologia.jpg", "Antropologia", 8 },
                    { 129, null, "https://dev.pl:2002/api/Image/Tags/etnografia.jpg", "Etnografia", 8 },
                    { 130, null, "https://dev.pl:2002/api/Image/Tags/etnologia.jpg", "Etnologia", 8 },
                    { 131, null, "https://dev.pl:2002/api/Image/Tags/etnomuzykologia.jpg", "Etnomuzykologia", 8 },
                    { 132, null, "https://dev.pl:2002/api/Image/Tags/język_polski.jpg", "Język polski", 11 },
                    { 133, null, "https://dev.pl:2002/api/Image/Tags/język_angielski.jpg", "Język angielski", 11 },
                    { 134, null, "https://dev.pl:2002/api/Image/Tags/język_niemiecki.jpg", "Język niemiecki", 11 },
                    { 135, null, "https://dev.pl:2002/api/Image/Tags/język_rosyjski.jpg", "Język rosyjski", 11 },
                    { 136, null, "https://dev.pl:2002/api/Image/Tags/język_francuski.jpg", "Język francuski", 11 },
                    { 137, null, "https://dev.pl:2002/api/Image/Tags/język_hiszpański.jpg", "Język hiszpański", 11 },
                    { 138, null, "https://dev.pl:2002/api/Image/Tags/język_włoski.jpg", "Język włoski", 11 },
                    { 139, null, "https://dev.pl:2002/api/Image/Tags/język_japoński.jpg", "Język japoński", 11 },
                    { 140, null, "https://dev.pl:2002/api/Image/Tags/język_chiński.jpg", "Język chiński", 11 },
                    { 141, null, "https://dev.pl:2002/api/Image/Tags/język_koreański.jpg", "Język koreański", 11 },
                    { 142, null, "https://dev.pl:2002/api/Image/Tags/język_portugalski.jpg", "Język portugalski", 11 },
                    { 143, null, "https://dev.pl:2002/api/Image/Tags/psychologia_poznawcza.jpg", "Psychologia poznawcza", 9 },
                    { 144, null, "https://dev.pl:2002/api/Image/Tags/psychologia_rozwojowa.jpg", "Psychologia rozwojowa", 9 },
                    { 145, null, "https://dev.pl:2002/api/Image/Tags/psychologia_społeczna.jpg", "Psychologia społeczna", 9 },
                    { 146, null, "https://dev.pl:2002/api/Image/Tags/psychologia_kliniczna.jpg", "Psychologia kliniczna", 9 },
                    { 147, null, "https://dev.pl:2002/api/Image/Tags/psychologia_pracy.jpg", "Psychologia pracy", 9 },
                    { 148, null, "https://dev.pl:2002/api/Image/Tags/psychologia_edukacyjna.jpg", "Psychologia edukacyjna", 9 },
                    { 149, null, "https://dev.pl:2002/api/Image/Tags/psychologia_sportu.jpg", "Psychologia sportu", 9 },
                    { 150, null, "https://dev.pl:2002/api/Image/Tags/psychologia_kryminalna.jpg", "Psychologia kryminalna", 9 },
                    { 151, null, "https://dev.pl:2002/api/Image/Tags/psychologia_medyczna.jpg", "Psychologia medyczna", 9 },
                    { 152, null, "https://dev.pl:2002/api/Image/Tags/malarstwo.jpg", "Malarstwo", 10 },
                    { 153, null, "https://dev.pl:2002/api/Image/Tags/rzeźba.jpg", "Rzeźba", 10 },
                    { 154, null, "https://dev.pl:2002/api/Image/Tags/grafika.jpg", "Grafika", 10 },
                    { 155, null, "https://dev.pl:2002/api/Image/Tags/fotografia.jpg", "Fotografia", 10 },
                    { 156, null, "https://dev.pl:2002/api/Image/Tags/film.jpg", "Film", 10 },
                    { 157, null, "https://dev.pl:2002/api/Image/Tags/muzyka.jpg", "Muzyka", 10 },
                    { 158, null, "https://dev.pl:2002/api/Image/Tags/teatr.jpg", "Teatr", 10 },
                    { 159, null, "https://dev.pl:2002/api/Image/Tags/literatura.jpg", "Literatura", 10 },
                    { 160, null, "https://dev.pl:2002/api/Image/Tags/prawo_podatkowe.jpg", "Prawo podatkowe", 12 },
                    { 161, null, "https://dev.pl:2002/api/Image/Tags/prawo_cywilne.jpg", "Prawo cywilne", 12 },
                    { 162, null, "https://dev.pl:2002/api/Image/Tags/prawo_karne.jpg", "Prawo karne", 12 },
                    { 163, null, "https://dev.pl:2002/api/Image/Tags/prawo_administracyjne.jpg", "Prawo administracyjne", 12 },
                    { 164, null, "https://dev.pl:2002/api/Image/Tags/prawo_pracy.jpg", "Prawo pracy", 12 },
                    { 165, null, "https://dev.pl:2002/api/Image/Tags/prawo_gospodarcze.jpg", "Prawo gospodarcze", 12 },
                    { 166, null, "https://dev.pl:2002/api/Image/Tags/prawo_handlowe.jpg", "Prawo handlowe", 12 },
                    { 167, null, "https://dev.pl:2002/api/Image/Tags/prawo_traktatów.jpg", "Prawo traktatów", 13 },
                    { 168, null, "https://dev.pl:2002/api/Image/Tags/prawo_dyplomatyczne.jpg", "Prawo dyplomatyczne", 13 },
                    { 169, null, "https://dev.pl:2002/api/Image/Tags/prawo_konsularne.jpg", "Prawo konsularne", 13 },
                    { 170, null, "https://dev.pl:2002/api/Image/Tags/prawo_kosmiczne.jpg", "Prawo kosmiczne", 13 },
                    { 171, null, "https://dev.pl:2002/api/Image/Tags/anatomia.jpg", "Anatomia", 14 },
                    { 172, null, "https://dev.pl:2002/api/Image/Tags/chirurgia.jpg", "Chirurgia", 14 },
                    { 173, null, "https://dev.pl:2002/api/Image/Tags/farmakologia.jpg", "Farmakologia", 14 },
                    { 174, null, "https://dev.pl:2002/api/Image/Tags/gastroenterologia.jpg", "Gastroenterologia", 14 },
                    { 175, null, "https://dev.pl:2002/api/Image/Tags/ginekologia.jpg", "Ginekologia", 14 },
                    { 176, null, "https://dev.pl:2002/api/Image/Tags/hematologia.jpg", "Hematologia", 14 },
                    { 177, null, "https://dev.pl:2002/api/Image/Tags/kardiologia.jpg", "Kardiologia", 14 },
                    { 178, null, "https://dev.pl:2002/api/Image/Tags/medycyna_rodzinna.jpg", "Medycyna rodzinna", 14 },
                    { 179, null, "https://dev.pl:2002/api/Image/Tags/medycyna_pracy.jpg", "Medycyna pracy", 14 },
                    { 180, null, "https://dev.pl:2002/api/Image/Tags/automatyka_i_robotyka.jpg", "Automatyka i robotyka", 15 },
                    { 181, null, "https://dev.pl:2002/api/Image/Tags/elektronika.jpg", "Elektronika", 15 },
                    { 182, null, "https://dev.pl:2002/api/Image/Tags/elektrotechnika.jpg", "Elektrotechnika", 15 },
                    { 183, null, "https://dev.pl:2002/api/Image/Tags/mechanika.jpg", "Mechanika", 15 },
                    { 184, null, "https://dev.pl:2002/api/Image/Tags/nauki_o_ziemi.jpg", "Nauki o Ziemi", 15 },
                    { 185, null, "https://dev.pl:2002/api/Image/Tags/nauki_o_materiałach.jpg", "Nauki o materiałach", 15 },
                    { 186, null, "https://dev.pl:2002/api/Image/Tags/nauki_o_transportie.jpg", "Nauki o transportie", 15 },
                    { 187, null, "https://dev.pl:2002/api/Image/Tags/nauki_o_wodzie.jpg", "Nauki o wodzie", 15 },
                    { 188, null, "https://dev.pl:2002/api/Image/Tags/nauki_o_zrównoważonym_rozwoju.jpg", "Nauki o zrównoważonym rozwoju", 15 },
                    { 189, null, "https://dev.pl:2002/api/Image/Tags/kryptowaluty.jpg", "Kryptowaluty", 16 },
                    { 190, null, "https://dev.pl:2002/api/Image/Tags/gielda.jpg", "Giełda", 16 },
                    { 191, null, "https://dev.pl:2002/api/Image/Tags/finanse.jpg", "Finanse", 16 },
                    { 192, null, "https://dev.pl:2002/api/Image/Tags/podatki.jpg", "Podatki", 16 },
                    { 193, null, "https://dev.pl:2002/api/Image/Tags/polityka_społeczna.jpg", "Polityka społeczna", 17 },
                    { 194, null, "https://dev.pl:2002/api/Image/Tags/polityka_zagraniczna.jpg", "Polityka zagraniczna", 17 },
                    { 195, null, "https://dev.pl:2002/api/Image/Tags/europeistyka.jpg", "Europeistyka", 17 },
                    { 196, null, "https://dev.pl:2002/api/Image/Tags/chrześcijaństwo.jpg", "Chrześcijaństwo", 18 },
                    { 197, null, "https://dev.pl:2002/api/Image/Tags/islam.jpg", "Islam", 18 },
                    { 198, null, "https://dev.pl:2002/api/Image/Tags/judaizm.jpg", "Judaizm", 18 },
                    { 199, null, "https://dev.pl:2002/api/Image/Tags/buddyzm.jpg", "Buddyzm", 18 },
                    { 200, null, "https://dev.pl:2002/api/Image/Tags/hinduizm.jpg", "Hinduizm", 18 },
                    { 201, null, "https://dev.pl:2002/api/Image/Tags/bahaizm.jpg", "Bahaizm", 18 },
                    { 202, null, "https://dev.pl:2002/api/Image/Tags/protestantyzm.jpg", "Protestantyzm", 18 },
                    { 203, null, "https://dev.pl:2002/api/Image/Tags/świadkowie_jehowy.jpg", "Świadkowie Jehowy", 18 },
                    { 204, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_wczesnoszkolne.jpg", "Wychowanie wczesnoszkolne", 19 },
                    { 205, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_przedszkolne.jpg", "Wychowanie przedszkolne", 19 },
                    { 206, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_szkolne.jpg", "Wychowanie szkolne", 19 },
                    { 207, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_gimnazjalne.jpg", "Wychowanie gimnazjalne", 19 },
                    { 208, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_licealne.jpg", "Wychowanie licealne", 19 },
                    { 209, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_studenckie.jpg", "Wychowanie studenckie", 19 },
                    { 210, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_dorosłych.jpg", "Wychowanie dorosłych", 19 },
                    { 211, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_specjalne.jpg", "Wychowanie specjalne", 19 },
                    { 212, null, "https://dev.pl:2002/api/Image/Tags/wychowanie_w_rodzinie.jpg", "Wychowanie w rodzinie", 19 },
                    { 213, null, "https://dev.pl:2002/api/Image/Tags/piłka_nożna.jpg", "Piłka nożna", 20 },
                    { 214, null, "https://dev.pl:2002/api/Image/Tags/koszykówka.jpg", "Koszykówka", 20 },
                    { 215, null, "https://dev.pl:2002/api/Image/Tags/siatkówka.jpg", "Siatkówka", 20 },
                    { 216, null, "https://dev.pl:2002/api/Image/Tags/tenis.jpg", "Tenis", 20 },
                    { 217, null, "https://dev.pl:2002/api/Image/Tags/siatkówka_plażowa.jpg", "Siatkówka plażowa", 20 },
                    { 218, null, "https://dev.pl:2002/api/Image/Tags/piłka_ręczna.jpg", "Piłka ręczna", 20 },
                    { 219, null, "https://dev.pl:2002/api/Image/Tags/piłka_siatkowa.jpg", "Piłka siatkowa", 20 },
                    { 220, null, "https://dev.pl:2002/api/Image/Tags/piłka_wodna.jpg", "Piłka wodna", 20 },
                    { 221, null, "https://dev.pl:2002/api/Image/Tags/kolarstwo.jpg", "Kolarstwo", 20 },
                    { 222, null, "https://dev.pl:2002/api/Image/Tags/bieganie.jpg", "Bieganie", 20 },
                    { 223, null, "https://dev.pl:2002/api/Image/Tags/turystyka_poznawczawa.jpg", "Turystyka poznawczawa", 21 },
                    { 224, null, "https://dev.pl:2002/api/Image/Tags/turystyka_rekreacyjna.jpg", "Turystyka rekreacyjna", 21 },
                    { 225, null, "https://dev.pl:2002/api/Image/Tags/turystyka_sportowa.jpg", "Turystyka sportowa", 21 },
                    { 226, null, "https://dev.pl:2002/api/Image/Tags/turystyka_biznesowa.jpg", "Turystyka biznesowa", 21 },
                    { 227, null, "https://dev.pl:2002/api/Image/Tags/turystyka_medyczna.jpg", "Turystyka medyczna", 21 },
                    { 228, null, "https://dev.pl:2002/api/Image/Tags/turystyka_kulturowa.jpg", "Turystyka kulturowa", 21 },
                    { 229, null, "https://dev.pl:2002/api/Image/Tags/turystyka_religijna.jpg", "Turystyka religijna", 21 },
                    { 230, null, "https://dev.pl:2002/api/Image/Tags/transport_lotniczy.jpg", "Transport lotniczy", 22 },
                    { 231, null, "https://dev.pl:2002/api/Image/Tags/transport_drogowy.jpg", "Transport drogowy", 22 },
                    { 232, null, "https://dev.pl:2002/api/Image/Tags/transport_wodny.jpg", "Transport wodny", 22 },
                    { 233, null, "https://dev.pl:2002/api/Image/Tags/transport_kolejowy.jpg", "Transport kolejowy", 22 },
                    { 234, null, "https://dev.pl:2002/api/Image/Tags/transport_morski.jpg", "Transport morski", 22 },
                    { 235, null, "https://dev.pl:2002/api/Image/Tags/planowanie_trasy.jpg", "Planowanie trasy", 22 },
                    { 236, null, "https://dev.pl:2002/api/Image/Tags/a320.jpg", "Airbus A320", 26 },
                    { 237, null, "https://dev.pl:2002/api/Image/Tags/a380.jpg", "Airbus A380", 26 },
                    { 238, null, "https://dev.pl:2002/api/Image/Tags/boeing_747.jpg", "Boeing 747", 26 },
                    { 239, null, "https://dev.pl:2002/api/Image/Tags/boeing_777.jpg", "Boeing 777", 26 },
                    { 240, null, "https://dev.pl:2002/api/Image/Tags/boeing_787.jpg", "Boeing 787", 26 },
                    { 241, null, "https://dev.pl:2002/api/Image/Tags/boeing_737.jpg", "Boeing 737", 26 },
                    { 242, null, "https://dev.pl:2002/api/Image/Tags/boeing_767.jpg", "Boeing 767", 26 },
                    { 243, null, "https://dev.pl:2002/api/Image/Tags/a350.jpg", "Airbus A350", 26 },
                    { 244, null, "https://dev.pl:2002/api/Image/Tags/reklama.jpg", "Reklama", 23 },
                    { 245, null, "https://dev.pl:2002/api/Image/Tags/marketing.jpg", "Marketing", 23 },
                    { 246, null, "https://dev.pl:2002/api/Image/Tags/pr.jpg", "PR", 23 },
                    { 247, null, "https://dev.pl:2002/api/Image/Tags/social_media.jpg", "Social Media", 23 },
                    { 248, null, "https://dev.pl:2002/api/Image/Tags/email_marketing.jpg", "E-mail marketing", 23 },
                    { 249, null, "https://dev.pl:2002/api/Image/Tags/marketing_wizualny.jpg", "Marketing wizualny", 23 },
                    { 250, null, "https://dev.pl:2002/api/Image/Tags/marketing_wewnetrzny.jpg", "Marketing wewnętrzny", 23 },
                    { 251, null, "https://dev.pl:2002/api/Image/Tags/marketing_internetowy.jpg", "Marketing internetowy", 23 },
                    { 252, null, "https://dev.pl:2002/api/Image/Tags/marketing_telewizyjny.jpg", "Marketing telewizyjny", 23 },
                    { 253, null, "https://dev.pl:2002/api/Image/Tags/marketing_mobilny.jpg", "Marketing mobilny", 23 },
                    { 254, null, "https://dev.pl:2002/api/Image/Tags/marketing_bezposredni.jpg", "Marketing bezpośredni", 23 },
                    { 255, null, "https://dev.pl:2002/api/Image/Tags/marketing_mix.jpg", "Marketing mix", 23 },
                    { 256, null, "https://dev.pl:2002/api/Image/Tags/marketing_relacji_publicznych.jpg", "Marketing relacji publicznych", 23 },
                    { 257, null, "https://dev.pl:2002/api/Image/Tags/marketing_strategiczny.jpg", "Marketing strategiczny", 23 },
                    { 258, null, "https://dev.pl:2002/api/Image/Tags/marketing_operacyjny.jpg", "Marketing operacyjny", 23 },
                    { 259, null, "https://dev.pl:2002/api/Image/Tags/marketing_terytorialny.jpg", "Marketing terytorialny", 23 },
                    { 260, null, "https://dev.pl:2002/api/Image/Tags/marketing_uslugowy.jpg", "Marketing usługowy", 23 },
                    { 261, null, "https://dev.pl:2002/api/Image/Tags/marketing_produktowy.jpg", "Marketing produktowy", 23 },
                    { 262, null, "https://dev.pl:2002/api/Image/Tags/marketing_segmentowy.jpg", "Marketing segmentowy", 23 },
                    { 263, null, "https://dev.pl:2002/api/Image/Tags/marketing_kanalowy.jpg", "Marketing kanałowy", 23 },
                    { 264, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_sledcze.jpg", "Dziennikrastwo śledcze", 24 },
                    { 265, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_internetowe.jpg", "Dziennikarstwo internetowe", 24 },
                    { 266, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_lokalne.jpg", "Dziennikarstwo lokalne", 24 },
                    { 267, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_miedzynarodowe.jpg", "Dziennikarstwo międzynarodowe", 24 },
                    { 268, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_prasowe.jpg", "Dziennikarstwo prasowe", 24 },
                    { 269, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_publicystyczne.jpg", "Dziennikarstwo publicystyczne", 24 },
                    { 270, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_radiowe.jpg", "Dziennikarstwo radiowe", 24 },
                    { 271, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_telewizyjne.jpg", "Dziennikarstwo telewizyjne", 24 },
                    { 272, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_wideo.jpg", "Dziennikarstwo wideo", 24 },
                    { 273, null, "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_spoleczne.jpg", "Dziennikarstwo społeczne", 24 },
                    { 274, null, "https://dev.pl:2002/api/Image/Tags/architektura_mieszkalna.jpg", "Architektura mieszkalna", 25 },
                    { 275, null, "https://dev.pl:2002/api/Image/Tags/architektura_wnetrz.jpg", "Architektura wnętrz", 25 },
                    { 276, null, "https://dev.pl:2002/api/Image/Tags/architektura_krajobrazu.jpg", "Architektura krajobrazu", 25 },
                    { 277, null, "https://dev.pl:2002/api/Image/Tags/architektura_przestrzeni_publicznej.jpg", "Architektura przestrzeni publicznej", 25 },
                    { 278, null, "https://dev.pl:2002/api/Image/Tags/architektura_przemyslowa.jpg", "Architektura przemysłowa", 25 },
                    { 279, null, "https://dev.pl:2002/api/Image/Tags/architektura_ogrodowa.jpg", "Architektura ogrodowa", 25 },
                    { 280, null, "https://dev.pl:2002/api/Image/Tags/architektura_przemyslowa.jpg", "Architektura przemysłowa", 25 },
                    { 281, null, "https://dev.pl:2002/api/Image/Tags/architektura_przestrzeni_publicznej.jpg", "Architektura przestrzeni publicznej", 25 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AcademicTitles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Universities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 26);
        }
    }
}
