using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;
using static System.Net.WebRequestMethods;

namespace UniQuanda.Infrastructure.Presistence.AppDb.DataSeeds
{
    public class TagDataSeed : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(
                new Tag()
                {
                    Id = 1,
                    Name = "Informatyka",
                    Description = "Programowanie i tematy pokrewne dotyczące informatyki"
                },
                new Tag
                {
                    Id = 2,
                    Name = "Matematyka",
                    Description = "Zagadnienia z dziedziny matematyki"
                },
                new Tag
                {
                    Id = 3,
                    Name = "Fizyka",
                    Description = "Zagadnienia z dziedziny fizyki"
                },
                new Tag
                {
                    Id = 4,
                    Name = "Chemia",
                    Description = "Zagadnienia z dziedziny chemii"
                },
                new Tag
                {
                    Id = 5,
                    Name = "Biologia",
                    Description = "Zagadnienia z dziedziny biologii"
                },
                new Tag
                {
                    Id = 6,
                    Name = "Geografia",
                    Description = "Zagadnienia z dziedziny geografii"
                },
                new Tag
                {
                    Id = 7,
                    Name = "Historia",
                    Description = "Zagadnienia z dziedziny historii"
                }, new Tag
                {
                    Id = 8,
                    Name = "Filozofia",
                    Description = "Zagadnienia z dziedziny filozofii"
                },
                new Tag
                {
                    Id = 9,
                    Name = "Psychologia",
                    Description = "Zagadnienia z dziedziny psychologii"
                },
                new Tag
                {
                    Id = 10,
                    Name = "Sztuka",
                    Description = "Zagadnienia z dziedziny dziedzin sztuki"
                },
                new Tag
                {
                    Id = 11,
                    Name = "Filologia",
                    Description = "Zagadnienia z dziedziny języków obcych"
                },
                new Tag
                {
                    Id = 12,
                    Name = "Prawo",
                    Description = "Zagadnienia z dziedziny polskiego prawa"
                },
                new Tag
                {
                    Id = 13,
                    Name = "Prawo międzynarodowe",
                    Description = "Zagadnienia z dziedziny międzynarodowego prawa"
                },
                new Tag
                {
                    Id = 14,
                    Name = "Medycyna",
                    Description = "Zagadnienia z dziedziny medycyny"
                },
                new Tag
                {
                    Id = 15,
                    Name = "Inżyneria",
                    Description = "Zagadnienia z dziedziny inżynierii"
                },
                new Tag
                {
                    Id = 16,
                    Name = "Ekonomia",
                    Description = "Zagadnienia z dziedziny ekonomii i finansów"
                },
                new Tag
                {
                    Id = 17,
                    Name = "Politologia",
                    Description = "Nauki polityczne, nauki o polityce, nauka o polityce. Nauka społeczna zajmująca się działalnością związaną ze sprawowaniem władzy polityczne"
                },
                new Tag
                {
                    Id = 18,
                    Name = "Teologia",
                    Description = "Zagadnienia z dziedziny religii"
                },
                new Tag
                {
                    Id = 19,
                    Name = "Pedagogika",
                    Description = "Zagadnienia z dziedziny edukacji i wychowania"
                },
                new Tag
                {
                    Id = 20,
                    Name = "Sport",
                    Description = "Zagadnienia z dziedziny sportu"
                },
                new Tag
                {
                    Id = 21,
                    Name = "Turystyka",
                    Description = "Zagadnienia z dziedziny turystyki i podróży"
                }, new Tag
                {
                    Id = 22,
                    Name = "Logistyka",
                    Description = "Zagadnienia z dziedziny logistyki i transportu"
                },
                new Tag
                {
                    Id = 23,
                    Name = "Marketing",
                    Description = "Zagadnienia z dziedziny marketingu i reklamy"
                },
                new Tag
                {
                    Id = 24,
                    Name = "Dziennikarstwo",
                    Description = "Zagadnienia z dziedziny dziennikarstwa"
                },
                new Tag
                {
                    Id = 25,
                    Name = "Architektura",
                    Description = "Zagadnienia z dziedziny architektury"
                },
                new Tag
                {
                    Id = 26,
                    Name = "Lotnictwo",
                    Description = "Zagadnienia związane z lotnictwem"
                },
                new Tag
                {
                    Id = 27,
                    Name = "Programowanie",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 28,
                    Name = "Bazy danych",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/bazy_danych.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 29,
                    Name = "Sieci komputerowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/sieci_komputerowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 30,
                    Name = "Systemy operacyjne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/systemy_operacyjne.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 31,
                    Name = "Angular",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/angular.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 32,
                    Name = "ASP.NET",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/asp_net.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 33,
                    Name = "C#",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/c_sharp.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 34,
                    Name = "C++",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/c_plus_plus.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 35,
                    Name = "Java",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/java.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 36,
                    Name = "JavaScript",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/javascript.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 37,
                    Name = "PHP",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/php.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 38,
                    Name = "Python",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/python.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 39,
                    Name = "Ruby",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/ruby.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 40,
                    Name = "SQL",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/sql.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 41,
                    Name = "TypeScript",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/typescript.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 42,
                    Name = "WPF",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wpf.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 43,
                    Name = "Xamarin",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/xamarin.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 44,
                    Name = "Inżynieria oprogramowania",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/inzynieria_oprogramowania.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 45,
                    Name = "Architektura oprogramowania",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_oprogramowania.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 46,
                    Name = "Testowanie oprogramowania",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/testowanie_oprogramowania.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 47,
                    Name = "Sztuczna inteligencja",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/sztuczna_inteligencja.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 48,
                    Name = "Programowanie obiektowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_obiektowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 49,
                    Name = "Programowanie funkcyjne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_funkcyjne.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 50,
                    Name = "Programowanie proceduralne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_proceduralne.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 51,
                    Name = "Programowanie zdarzeniowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_zdarzeniowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 52,
                    Name = "Programowanie asynchroniczne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_asynchroniczne.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 53,
                    Name = "Programowanie równoległe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_równoległe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 54,
                    Name = "Programowanie wielowątkowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_wielowątkowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 55,
                    Name = "Programowanie wieloprocesowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_wieloprocesowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 56,
                    Name = "Programowanie wielokomputerowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_wielokomputerowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 57,
                    Name = "Programowanie rozproszone",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_rozproszone.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 58,
                    Name = "Programowanie rozproszone",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_rozproszone.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 59,
                    Name = "Programowanie równoległe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_równoległe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 60,
                    Name = "Programowanie wielowątkowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/programowanie_wielowątkowe.jpg",
                    ParentTagId = 1
                },
                new Tag
                {
                    Id = 61,
                    Name = "Matematyka dyskretna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/matematyka_dyskretna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 62,
                    Name = "Planimetria",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/planimetria.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 63,
                    Name = "Analiza matematyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_matematyczna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 64,
                    Name = "Algebra liniowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/algebra_liniowa.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 65,
                    Name = "Geometria",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geometria.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 66,
                    Name = "Logika matematyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/logika_matematyczna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 67,
                    Name = "Teoria liczb",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_liczb.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 68,
                    Name = "Teoria mnogości",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_mnogości.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 69,
                    Name = "Teoria grafów",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_grafów.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 70,
                    Name = "Teoria gier",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_gier.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 71,
                    Name = "Teoria kodowania",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_kodowania.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 72,
                    Name = "Teoria obliczeń",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_obliczeń.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 73,
                    Name = "Teoria algorytmów",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teoria_algorytmów.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 74,
                    Name = "Algebra nieliniowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/algebra_nieliniowa.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 75,
                    Name = "Staystyka",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/staystyka.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 76,
                    Name = "Matematyka stosowana",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/matematyka_stosowana.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 77,
                    Name = "Topologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/topologia.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 78,
                    Name = "Analiza numeryczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_numeryczna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 79,
                    Name = "Analiza funkcjonalna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_funkcjonalna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 80,
                    Name = "Analiza zespolona",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_zespolona.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 81,
                    Name = "Analiza Fouriera",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_fouriera.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 82,
                    Name = "Analiza graniczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_graniczna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 83,
                    Name = "Analiza różnicowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_różnicowa.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 84,
                    Name = "Analiza integralna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_integralna.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 85,
                    Name = "Analiza funkcji wielu zmiennych",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_funkcji_wielu_zmiennych.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 86,
                    Name = "Analiza funkcji rzeczywistych",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_funkcji_rzeczywistych.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 87,
                    Name = "Analiza funkcji zespolonych",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/analiza_funkcji_zespolonych.jpg",
                    ParentTagId = 2
                },
                new Tag
                {
                    Id = 88,
                    Name = "Fizyka Kwantowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_kwantowa.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 89,
                    Name = "Fizyka atomowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_atomowa.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 90,
                    Name = "Fizyka molekularna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_molekularna.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 91,
                    Name = "Fizyka ciała stałego",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_stałego.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 92,
                    Name = "Fizyka ciała rzadkiego",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_rzadkiego.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 93,
                    Name = "Fizyka ciała płynnego",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_płynnego.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 94,
                    Name = "Fizyka ciała gazowego",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_gazowego.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 95,
                    Name = "Fizyka ciała promieniotwórczego",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_promieniotwórczego.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 96,
                    Name = "Fizyka ciała złożonego",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fizyka_ciała_złożonego.jpg",
                    ParentTagId = 3
                },
                new Tag
                {
                    Id = 97,
                    Name = "Chemia organiczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chemia_organiczna.jpg",
                    ParentTagId = 4
                },
                new Tag
                {
                    Id = 98,
                    Name = "Chemia analityczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chemia_analityczna.jpg",
                    ParentTagId = 4
                },
                new Tag
                {
                    Id = 99,
                    Name = "Chemia fizyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chemia_fizyczna.jpg",
                    ParentTagId = 4
                },
                new Tag
                {
                    Id = 100,
                    Name = "Chemia inorganiczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chemia_inorganiczna.jpg",
                    ParentTagId = 4
                },
                new Tag
                {
                    Id = 101,
                    Name = "Chemia biologiczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chemia_biologiczna.jpg",
                    ParentTagId = 4
                },
                new Tag
                {
                    Id = 102,
                    Name = "Biologia molekularna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_molekularna.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 103,
                    Name = "Biologia komórkowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_komórkowa.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 104,
                    Name = "Biologia ewolucyjna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_ewolucyjna.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 105,
                    Name = "Biologia systematyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_systematyczna.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 106,
                    Name = "Biologia populacyjna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_populacyjna.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 107,
                    Name = "Biologia ekologiczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_ekologiczna.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 108,
                    Name = "Biologia ekonomii",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_ekonomii.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 109,
                    Name = "Biologia roślin",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_roślin.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 110,
                    Name = "Biologia zwierząt",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/biologia_zwierząt.jpg",
                    ParentTagId = 5
                },
                new Tag
                {
                    Id = 111,
                    Name = "Geomorfologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geomorfologia.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 112,
                    Name = "Geologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geologia.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 113,
                    Name = "Geografia fizyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_fizyczna.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 114,
                    Name = "Geografia polityczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_polityczna.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 115,
                    Name = "Geografia ekonomiczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_ekonomiczna.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 116,
                    Name = "Geografia społeczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_społeczna.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 117,
                    Name = "Geografia kulturowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_kulturowa.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 118,
                    Name = "Geografia regionalna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_regionalna.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 119,
                    Name = "Geografia turystyki",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_turystyki.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 120,
                    Name = "Geografia fizyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/geografia_fizyczna.jpg",
                    ParentTagId = 6
                },
                new Tag
                {
                    Id = 121,
                    Name = "Historia starożytna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/historia_starożytna.jpg",
                    ParentTagId = 7
                },
                new Tag
                {
                    Id = 122,
                    Name = "Historia średniowiecza",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/historia_średniowiecza.jpg",
                    ParentTagId = 7
                },
                new Tag
                {
                    Id = 123,
                    Name = "Historia nowożytna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/historia_nowożytna.jpg",
                    ParentTagId = 7
                },
                new Tag
                {
                    Id = 124,
                    Name = "Historia współczesna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/historia_współczesna.jpg",
                    ParentTagId = 7
                },
                new Tag
                {
                    Id = 126,
                    Name = "Nauki społeczne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/nauki_społeczne.jpg",
                    ParentTagId = 8
                },
                new Tag
                {
                    Id = 127,
                    Name = "Socjologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/socjologia.jpg",
                    ParentTagId = 8
                },
                new Tag
                {
                    Id = 128,
                    Name = "Antropologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/antropologia.jpg",
                    ParentTagId = 8
                },
                new Tag
                {
                    Id = 129,
                    Name = "Etnografia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/etnografia.jpg",
                    ParentTagId = 8
                },
                new Tag
                {
                    Id = 130,
                    Name = "Etnologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/etnologia.jpg",
                    ParentTagId = 8
                },
                new Tag
                {
                    Id = 131,
                    Name = "Etnomuzykologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/etnomuzykologia.jpg",
                    ParentTagId = 8
                },
                new Tag
                {
                    Id = 132,
                    Name = "Język polski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_polski.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 133,
                    Name = "Język angielski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_angielski.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 134,
                    Name = "Język niemiecki",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_niemiecki.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 135,
                    Name = "Język rosyjski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_rosyjski.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 136,
                    Name = "Język francuski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_francuski.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 137,
                    Name = "Język hiszpański",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_hiszpański.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 138,
                    Name = "Język włoski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_włoski.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 139,
                    Name = "Język japoński",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_japoński.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 140,
                    Name = "Język chiński",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_chiński.jpg",

                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 141,
                    Name = "Język koreański",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_koreański.jpg",

                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 142,
                    Name = "Język portugalski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/język_portugalski.jpg",
                    ParentTagId = 11
                },
                new Tag
                {
                    Id = 143,
                    Name = "Psychologia poznawcza",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_poznawcza.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 144,
                    Name = "Psychologia rozwojowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_rozwojowa.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 145,
                    Name = "Psychologia społeczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_społeczna.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 146,
                    Name = "Psychologia kliniczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_kliniczna.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 147,
                    Name = "Psychologia pracy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_pracy.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 148,
                    Name = "Psychologia edukacyjna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_edukacyjna.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 149,
                    Name = "Psychologia sportu",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_sportu.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 150,
                    Name = "Psychologia kryminalna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_kryminalna.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 151,
                    Name = "Psychologia medyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/psychologia_medyczna.jpg",
                    ParentTagId = 9
                },
                new Tag
                {
                    Id = 152,
                    Name = "Malarstwo",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/malarstwo.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 153,
                    Name = "Rzeźba",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/rzeźba.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 154,
                    Name = "Grafika",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/grafika.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 155,
                    Name = "Fotografia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/fotografia.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 156,
                    Name = "Film",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/film.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 157,
                    Name = "Muzyka",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/muzyka.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 158,
                    Name = "Teatr",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/teatr.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 159,
                    Name = "Literatura",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/literatura.jpg",
                    ParentTagId = 10
                },
                new Tag
                {
                    Id = 160,
                    Name = "Prawo podatkowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_podatkowe.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 161,
                    Name = "Prawo cywilne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_cywilne.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 162,
                    Name = "Prawo karne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_karne.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 163,
                    Name = "Prawo administracyjne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_administracyjne.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 164,
                    Name = "Prawo pracy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_pracy.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 165,
                    Name = "Prawo gospodarcze",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_gospodarcze.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 166,
                    Name = "Prawo handlowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_handlowe.jpg",
                    ParentTagId = 12
                },
                new Tag
                {
                    Id = 167,
                    Name = "Prawo traktatów",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_traktatów.jpg",
                    ParentTagId = 13
                },
                new Tag
                {
                    Id = 168,
                    Name = "Prawo dyplomatyczne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_dyplomatyczne.jpg",
                    ParentTagId = 13
                },
                new Tag
                {
                    Id = 169,
                    Name = "Prawo konsularne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_konsularne.jpg",
                    ParentTagId = 13
                },
                new Tag
                {
                    Id = 170,
                    Name = "Prawo kosmiczne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/prawo_kosmiczne.jpg",
                    ParentTagId = 13
                },
                new Tag
                {
                    Id = 171,
                    Name = "Anatomia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/anatomia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 172,
                    Name = "Chirurgia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chirurgia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 173,
                    Name = "Farmakologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/farmakologia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 174,
                    Name = "Gastroenterologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/gastroenterologia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 175,
                    Name = "Ginekologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/ginekologia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 176,
                    Name = "Hematologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/hematologia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 177,
                    Name = "Kardiologia",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/kardiologia.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 178,
                    Name = "Medycyna rodzinna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/medycyna_rodzinna.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 179,
                    Name = "Medycyna pracy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/medycyna_pracy.jpg",
                    ParentTagId = 14
                },
                new Tag
                {
                    Id = 180,
                    Name = "Automatyka i robotyka",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/automatyka_i_robotyka.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 181,
                    Name = "Elektronika",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/elektronika.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 182,
                    Name = "Elektrotechnika",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/elektrotechnika.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 183,
                    Name = "Mechanika",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/mechanika.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 184,
                    Name = "Nauki o Ziemi",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/nauki_o_ziemi.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 185,
                    Name = "Nauki o materiałach",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/nauki_o_materiałach.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 186,
                    Name = "Nauki o transportie",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/nauki_o_transportie.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 187,
                    Name = "Nauki o wodzie",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/nauki_o_wodzie.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 188,
                    Name = "Nauki o zrównoważonym rozwoju",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/nauki_o_zrównoważonym_rozwoju.jpg",
                    ParentTagId = 15
                },
                new Tag
                {
                    Id = 189,
                    Name = "Kryptowaluty",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/kryptowaluty.jpg",
                    ParentTagId = 16
                },
                new Tag
                {
                    Id = 190,
                    Name = "Giełda",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/gielda.jpg",
                    ParentTagId = 16
                },
                new Tag
                {
                    Id = 191,
                    Name = "Finanse",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/finanse.jpg",
                    ParentTagId = 16
                },
                new Tag
                {
                    Id = 192,
                    Name = "Podatki",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/podatki.jpg",
                    ParentTagId = 16
                },
                new Tag
                {
                    Id = 193,
                    Name = "Polityka społeczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/polityka_społeczna.jpg",
                    ParentTagId = 17
                },
                new Tag
                {
                    Id = 194,
                    Name = "Polityka zagraniczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/polityka_zagraniczna.jpg",
                    ParentTagId = 17
                },
                new Tag
                {
                    Id = 195,
                    Name = "Europeistyka",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/europeistyka.jpg",
                    ParentTagId = 17
                },
                new Tag
                {
                    Id = 196,
                    Name = "Chrześcijaństwo",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/chrześcijaństwo.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 197,
                    Name = "Islam",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/islam.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 198,
                    Name = "Judaizm",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/judaizm.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 199,
                    Name = "Buddyzm",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/buddyzm.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 200,
                    Name = "Hinduizm",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/hinduizm.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 201,
                    Name = "Bahaizm",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/bahaizm.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 202,
                    Name = "Protestantyzm",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/protestantyzm.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 203,
                    Name = "Świadkowie Jehowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/świadkowie_jehowy.jpg",
                    ParentTagId = 18
                },
                new Tag
                {
                    Id = 204,
                    Name = "Wychowanie wczesnoszkolne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_wczesnoszkolne.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 205,
                    Name = "Wychowanie przedszkolne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_przedszkolne.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 206,
                    Name = "Wychowanie szkolne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_szkolne.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 207,
                    Name = "Wychowanie gimnazjalne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_gimnazjalne.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 208,
                    Name = "Wychowanie licealne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_licealne.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 209,
                    Name = "Wychowanie studenckie",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_studenckie.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 210,
                    Name = "Wychowanie dorosłych",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_dorosłych.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 211,
                    Name = "Wychowanie specjalne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_specjalne.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 212,
                    Name = "Wychowanie w rodzinie",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/wychowanie_w_rodzinie.jpg",
                    ParentTagId = 19
                },
                new Tag
                {
                    Id = 213,
                    Name = "Piłka nożna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/piłka_nożna.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 214,
                    Name = "Koszykówka",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/koszykówka.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 215,
                    Name = "Siatkówka",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/siatkówka.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 216,
                    Name = "Tenis",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/tenis.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 217,
                    Name = "Siatkówka plażowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/siatkówka_plażowa.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 218,
                    Name = "Piłka ręczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/piłka_ręczna.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 219,
                    Name = "Piłka siatkowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/piłka_siatkowa.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 220,
                    Name = "Piłka wodna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/piłka_wodna.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 221,
                    Name = "Kolarstwo",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/kolarstwo.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 222,
                    Name = "Bieganie",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/bieganie.jpg",
                    ParentTagId = 20
                },
                new Tag
                {
                    Id = 223,
                    Name = "Turystyka poznawczawa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_poznawczawa.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 224,
                    Name = "Turystyka rekreacyjna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_rekreacyjna.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 225,
                    Name = "Turystyka sportowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_sportowa.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 226,
                    Name = "Turystyka biznesowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_biznesowa.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 227,
                    Name = "Turystyka medyczna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_medyczna.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 228,
                    Name = "Turystyka kulturowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_kulturowa.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 229,
                    Name = "Turystyka religijna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/turystyka_religijna.jpg",
                    ParentTagId = 21
                },
                new Tag
                {
                    Id = 230,
                    Name = "Transport lotniczy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/transport_lotniczy.jpg",
                    ParentTagId = 22
                },
                new Tag
                {
                    Id = 231,
                    Name = "Transport drogowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/transport_drogowy.jpg",
                    ParentTagId = 22
                },
                new Tag
                {
                    Id = 232,
                    Name = "Transport wodny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/transport_wodny.jpg",
                    ParentTagId = 22
                },
                new Tag
                {
                    Id = 233,
                    Name = "Transport kolejowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/transport_kolejowy.jpg",
                    ParentTagId = 22
                },
                new Tag
                {
                    Id = 234,
                    Name = "Transport morski",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/transport_morski.jpg",
                    ParentTagId = 22
                },
                new Tag
                {
                    Id = 235,
                    Name = "Planowanie trasy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/planowanie_trasy.jpg",
                    ParentTagId = 22
                },
                new Tag
                {
                    Id = 236,
                    Name = "Airbus A320",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/a320.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 237,
                    Name = "Airbus A380",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/a380.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 238,
                    Name = "Boeing 747",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/boeing_747.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 239,
                    Name = "Boeing 777",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/boeing_777.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 240,
                    Name = "Boeing 787",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/boeing_787.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 241,
                    Name = "Boeing 737",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/boeing_737.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 242,
                    Name = "Boeing 767",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/boeing_767.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 243,
                    Name = "Airbus A350",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/a350.jpg",
                    ParentTagId = 26
                },
                new Tag
                {
                    Id = 244,
                    Name = "Reklama",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/reklama.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 245,
                    Name = "Marketing",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 246,
                    Name = "PR",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/pr.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 247,
                    Name = "Social Media",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/social_media.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 248,
                    Name = "E-mail marketing",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/email_marketing.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 249,
                    Name = "Marketing wizualny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_wizualny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 250,
                    Name = "Marketing wewnętrzny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_wewnetrzny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 251,
                    Name = "Marketing internetowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_internetowy.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 252,
                    Name = "Marketing telewizyjny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_telewizyjny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 253,
                    Name = "Marketing mobilny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_mobilny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 254,
                    Name = "Marketing bezpośredni",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_bezposredni.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 255,
                    Name = "Marketing mix",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_mix.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 256,
                    Name = "Marketing relacji publicznych",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_relacji_publicznych.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 257,
                    Name = "Marketing strategiczny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_strategiczny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 258,
                    Name = "Marketing operacyjny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_operacyjny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 259,
                    Name = "Marketing terytorialny",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_terytorialny.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 260,
                    Name = "Marketing usługowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_uslugowy.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 261,
                    Name = "Marketing produktowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_produktowy.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 262,
                    Name = "Marketing segmentowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_segmentowy.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 263,
                    Name = "Marketing kanałowy",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/marketing_kanalowy.jpg",
                    ParentTagId = 23
                },
                new Tag
                {
                    Id = 264,
                    Name = "Dziennikrastwo śledcze",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_sledcze.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 265,
                    Name = "Dziennikarstwo internetowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_internetowe.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 266,
                    Name = "Dziennikarstwo lokalne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_lokalne.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 267,
                    Name = "Dziennikarstwo międzynarodowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_miedzynarodowe.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 268,
                    Name = "Dziennikarstwo prasowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_prasowe.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 269,
                    Name = "Dziennikarstwo publicystyczne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_publicystyczne.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 270,
                    Name = "Dziennikarstwo radiowe",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_radiowe.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 271,
                    Name = "Dziennikarstwo telewizyjne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_telewizyjne.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 272,
                    Name = "Dziennikarstwo wideo",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_wideo.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 273,
                    Name = "Dziennikarstwo społeczne",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/dziennikarstwo_spoleczne.jpg",
                    ParentTagId = 24
                },
                new Tag
                {
                    Id = 274,
                    Name = "Architektura mieszkalna",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_mieszkalna.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 275,
                    Name = "Architektura wnętrz",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_wnetrz.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 276,
                    Name = "Architektura krajobrazu",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_krajobrazu.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 277,
                    Name = "Architektura przestrzeni publicznej",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_przestrzeni_publicznej.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 278,
                    Name = "Architektura przemysłowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_przemyslowa.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 279,
                    Name = "Architektura ogrodowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_ogrodowa.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 280,
                    Name = "Architektura przemysłowa",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_przemyslowa.jpg",
                    ParentTagId = 25
                },
                new Tag
                {
                    Id = 281,
                    Name = "Architektura przestrzeni publicznej",
                    ImageUrl = "https://dev.pl:2002/api/Image/Tags/architektura_przestrzeni_publicznej.jpg",
                    ParentTagId = 25
                }
            );
        }
    }
}