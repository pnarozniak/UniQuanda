using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

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
                    Description = "Programowanie i tematy pokrewne dotyczące informatyki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg"
                },
                new Tag
                {
                    Id = 2,
                    Name = "Matematyka",
                    Description = "Zagadnienia z dziedziny matematyki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg"
                },
                new Tag
                {
                    Id = 3,
                    Name = "Fizyka",
                    Description = "Zagadnienia z dziedziny fizyki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg"
                },
                new Tag
                {
                    Id = 4,
                    Name = "Chemia",
                    Description = "Zagadnienia z dziedziny chemii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia.jpg"
                },
                new Tag
                {
                    Id = 5,
                    Name = "Biologia",
                    Description = "Zagadnienia z dziedziny biologii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia.jpg"
                },
                new Tag
                {
                    Id = 6,
                    Name = "Geografia",
                    Description = "Zagadnienia z dziedziny geografii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg"
                },
                new Tag
                {
                    Id = 7,
                    Name = "Historia",
                    Description = "Zagadnienia z dziedziny historii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/historia.jpg"
                }, new Tag
                {
                    Id = 8,
                    Name = "Filozofia",
                    Description = "Zagadnienia z dziedziny filozofii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/filozofia.jpg"
                },
                new Tag
                {
                    Id = 9,
                    Name = "Psychologia",
                    Description = "Zagadnienia z dziedziny psychologii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/pscyhologia.jpg"
                },
                new Tag
                {
                    Id = 10,
                    Name = "Sztuka",
                    Description = "Zagadnienia z dziedziny dziedzin sztuki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/sztuka.jpg"
                },
                new Tag
                {
                    Id = 11,
                    Name = "Filologia",
                    Description = "Zagadnienia z dziedziny języków obcych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/filologia.jpg"
                },
                new Tag
                {
                    Id = 12,
                    Name = "Prawo",
                    Description = "Zagadnienia z dziedziny polskiego prawa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg"
                },
                new Tag
                {
                    Id = 13,
                    Name = "Prawo międzynarodowe",
                    Description = "Zagadnienia z dziedziny międzynarodowego prawa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo_międzynarodowe.jpg"
                },
                new Tag
                {
                    Id = 14,
                    Name = "Medycyna",
                    Description = "Zagadnienia z dziedziny medycyny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/medycyna.jpg"
                },
                new Tag
                {
                    Id = 15,
                    Name = "Inżyneria",
                    Description = "Zagadnienia z dziedziny inżynierii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/inżynieria.jpg"
                },
                new Tag
                {
                    Id = 16,
                    Name = "Ekonomia",
                    Description = "Zagadnienia z dziedziny ekonomii i finansów",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/ekonomia.jpg"
                },
                new Tag
                {
                    Id = 17,
                    Name = "Politologia",
                    Description = "Nauki polityczne, nauki o polityce, nauka o polityce. Nauka społeczna zajmująca się działalnością związaną ze sprawowaniem władzy polityczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/politologia.jpg"
                },
                new Tag
                {
                    Id = 18,
                    Name = "Teologia",
                    Description = "Zagadnienia z dziedziny religii",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teologia.jpg"
                },
                new Tag
                {
                    Id = 19,
                    Name = "Pedagogika",
                    Description = "Zagadnienia z dziedziny edukacji i wychowania",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/pedagogika.jpg"
                },
                new Tag
                {
                    Id = 20,
                    Name = "Sport",
                    Description = "Zagadnienia z dziedziny sportu",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/sport.jpg"
                },
                new Tag
                {
                    Id = 21,
                    Name = "Turystyka",
                    Description = "Zagadnienia z dziedziny turystyki i podróży",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg"
                }, new Tag
                {
                    Id = 22,
                    Name = "Logistyka",
                    Description = "Zagadnienia z dziedziny logistyki i transportu",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/logistyka.jpg"
                },
                new Tag
                {
                    Id = 23,
                    Name = "Marketing",
                    Description = "Zagadnienia z dziedziny marketingu i reklamy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg"
                },
                new Tag
                {
                    Id = 24,
                    Name = "Dziennikarstwo",
                    Description = "Zagadnienia z dziedziny dziennikarstwa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg"
                },
                new Tag
                {
                    Id = 25,
                    Name = "Architektura",
                    Description = "Zagadnienia z dziedziny architektury",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura.jpg"
                },
                new Tag
                {
                    Id = 26,
                    Name = "Lotnictwo",
                    Description = "Zagadnienia związane z lotnictwem",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/lotnictwo.jpg"
                },
                new Tag()
                {
                    Id = 27,
                    ParentTagId = 1,
                    Name = "Programowanie",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/programowanie.jpg",
                },
                new Tag()
                {
                    Id = 28,
                    ParentTagId = 1,
                    Name = "Bazy danych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/bazy_danych.jpg",
                },
                new Tag()
                {
                    Id = 29,
                    ParentTagId = 1,
                    Name = "Sieci komputerowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/sieci_komputerowe.jpg",
                },
                new Tag()
                {
                    Id = 30,
                    ParentTagId = 1,
                    Name = "Systemy operacyjne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/systemy_operacyjne.jpg",
                },
                new Tag()
                {
                    Id = 31,
                    ParentTagId = 1,
                    Name = "Angular",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/angular.jpg",
                },
                new Tag()
                {
                    Id = 32,
                    ParentTagId = 1,
                    Name = "ASP.NET",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/asp_net.jpg",
                },
                new Tag()
                {
                    Id = 33,
                    ParentTagId = 1,
                    Name = "C#",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/c_sharp.jpg",
                },
                new Tag()
                {
                    Id = 34,
                    ParentTagId = 1,
                    Name = "C++",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/c_plus_plus.jpg",
                },
                new Tag()
                {
                    Id = 35,
                    ParentTagId = 1,
                    Name = "Java",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/java.jpg",
                },
                new Tag()
                {
                    Id = 36,
                    ParentTagId = 1,
                    Name = "JavaScript",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/javascript.jpg",
                },
                new Tag()
                {
                    Id = 37,
                    ParentTagId = 1,
                    Name = "PHP",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/php.jpg",
                },
                new Tag()
                {
                    Id = 38,
                    ParentTagId = 1,
                    Name = "Python",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/python.jpg",
                },
                new Tag()
                {
                    Id = 39,
                    ParentTagId = 1,
                    Name = "Ruby",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/ruby.jpg",
                },
                new Tag()
                {
                    Id = 40,
                    ParentTagId = 1,
                    Name = "SQL",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/sql.jpg",
                },
                new Tag()
                {
                    Id = 41,
                    ParentTagId = 1,
                    Name = "TypeScript",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/typescript.jpg",
                },
                new Tag()
                {
                    Id = 42,
                    ParentTagId = 1,
                    Name = "WPF",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wpf.jpg",
                },
                new Tag()
                {
                    Id = 43,
                    ParentTagId = 1,
                    Name = "PostgreSQL",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/postgresql.jpg",
                },
                new Tag()
                {
                    Id = 44,
                    ParentTagId = 1,
                    Name = "AWS",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/aws.jpg",
                },
                new Tag()
                {
                    Id = 45,
                    ParentTagId = 1,
                    Name = "Docker",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/docker.jpg",
                },
                new Tag()
                {
                    Id = 46,
                    ParentTagId = 1,
                    Name = "Ada",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/ada.jpg",
                },
                new Tag()
                {
                    Id = 47,
                    ParentTagId = 1,
                    Name = "CSS",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/css.jpg",
                },
                new Tag()
                {
                    Id = 48,
                    ParentTagId = 1,
                    Name = "HTML",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/html.jpg",
                },
                new Tag()
                {
                    Id = 49,
                    ParentTagId = 1,
                    Name = "Node",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/node.jpg",
                },
                new Tag()
                {
                    Id = 50,
                    ParentTagId = 1,
                    Name = "Kafka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/kafka.jpg",
                },
                new Tag()
                {
                    Id = 51,
                    ParentTagId = 1,
                    Name = "React",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/react.jpg",
                },
                new Tag()
                {
                    Id = 52,
                    ParentTagId = 1,
                    Name = "Xamarin",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/xamarin.jpg",
                },
                new Tag()
                {
                    Id = 53,
                    ParentTagId = 1,
                    Name = "Inżynieria oprogramowania",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/inzynieria_oprogramowania.jpg",
                },
                new Tag()
                {
                    Id = 54,
                    ParentTagId = 1,
                    Name = "Architektura oprogramowania",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 55,
                    ParentTagId = 1,
                    Name = "Testowanie oprogramowania",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 56,
                    ParentTagId = 1,
                    Name = "Sztuczna inteligencja",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/sztuczna_inteligencja.jpg",
                },
                new Tag()
                {
                    Id = 57,
                    ParentTagId = 1,
                    Name = "Programowanie obiektowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 58,
                    ParentTagId = 1,
                    Name = "Programowanie funkcyjne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 59,
                    ParentTagId = 1,
                    Name = "Programowanie proceduralne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 60,
                    ParentTagId = 1,
                    Name = "Programowanie zdarzeniowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 61,
                    ParentTagId = 1,
                    Name = "Programowanie asynchroniczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 62,
                    ParentTagId = 1,
                    Name = "Programowanie równoległe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 63,
                    ParentTagId = 1,
                    Name = "Programowanie wielowątkowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 64,
                    ParentTagId = 1,
                    Name = "Programowanie wieloprocesowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 65,
                    ParentTagId = 1,
                    Name = "Sieci komputerowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 66,
                    ParentTagId = 1,
                    Name = "Programowanie rozproszone",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/informatyka.jpg",
                },
                new Tag()
                {
                    Id = 67,
                    ParentTagId = 2,
                    Name = "Matematyka dyskretna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 68,
                    ParentTagId = 2,
                    Name = "Planimetria",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/planimetria.jpg",
                },
                new Tag()
                {
                    Id = 69,
                    ParentTagId = 2,
                    Name = "Analiza matematyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 70,
                    ParentTagId = 2,
                    Name = "Algebra liniowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 71,
                    ParentTagId = 2,
                    Name = "Geometria",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geometria.jpg",
                },
                new Tag()
                {
                    Id = 72,
                    ParentTagId = 2,
                    Name = "Logika matematyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/logika_matematyczna.jpg",
                },
                new Tag()
                {
                    Id = 73,
                    ParentTagId = 2,
                    Name = "Teoria liczb",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_liczb.jpg",
                },
                new Tag()
                {
                    Id = 74,
                    ParentTagId = 2,
                    Name = "Teoria mnogości",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 75,
                    ParentTagId = 2,
                    Name = "Teoria grafów",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 76,
                    ParentTagId = 2,
                    Name = "Teoria gier",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_gier.jpg",
                },
                new Tag()
                {
                    Id = 77,
                    ParentTagId = 2,
                    Name = "Teoria kodowania",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_kodowania.jpg",
                },
                new Tag()
                {
                    Id = 78,
                    ParentTagId = 2,
                    Name = "Teoria obliczeń",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teoria_obliczeń.jpg",
                },
                new Tag()
                {
                    Id = 79,
                    ParentTagId = 2,
                    Name = "Teoria algorytmów",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 80,
                    ParentTagId = 2,
                    Name = "Algebra nieliniowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 81,
                    ParentTagId = 2,
                    Name = "Staystyka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/staystyka.jpg",
                },
                new Tag()
                {
                    Id = 82,
                    ParentTagId = 2,
                    Name = "Matematyka stosowana",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 83,
                    ParentTagId = 2,
                    Name = "Topologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 84,
                    ParentTagId = 2,
                    Name = "Analiza numeryczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 85,
                    ParentTagId = 2,
                    Name = "Analiza funkcjonalna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 86,
                    ParentTagId = 2,
                    Name = "Analiza zespolona",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 87,
                    ParentTagId = 2,
                    Name = "Analiza Fouriera",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 88,
                    ParentTagId = 2,
                    Name = "Analiza graniczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 89,
                    ParentTagId = 2,
                    Name = "Analiza różnicowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 90,
                    ParentTagId = 2,
                    Name = "Analiza integralna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 91,
                    ParentTagId = 2,
                    Name = "Analiza funkcji wielu zmiennych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 92,
                    ParentTagId = 2,
                    Name = "Analiza funkcji rzeczywistych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 93,
                    ParentTagId = 2,
                    Name = "Analiza funkcji zespolonych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/matematyka.jpg",
                },
                new Tag()
                {
                    Id = 94,
                    ParentTagId = 3,
                    Name = "Fizyka Kwantowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_kwantowa.jpg",
                },
                new Tag()
                {
                    Id = 95,
                    ParentTagId = 3,
                    Name = "Fizyka atomowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg",
                },
                new Tag()
                {
                    Id = 96,
                    ParentTagId = 3,
                    Name = "Fizyka molekularna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg",
                },
                new Tag()
                {
                    Id = 97,
                    ParentTagId = 3,
                    Name = "Fizyka ciała stałego",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_stałego.jpg",
                },
                new Tag()
                {
                    Id = 98,
                    ParentTagId = 3,
                    Name = "Fizyka ciała płynnego",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_płynnego.jpg",
                },
                new Tag()
                {
                    Id = 99,
                    ParentTagId = 3,
                    Name = "Fizyka ciała gazowego",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_gazowego.jpg",
                },
                new Tag()
                {
                    Id = 100,
                    ParentTagId = 3,
                    Name = "Fizyka ciała promieniotwórczego",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka_ciała_promieniotwórczego.jpg",
                },
                new Tag()
                {
                    Id = 101,
                    ParentTagId = 3,
                    Name = "Fizyka ciała złożonego",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fizyka.jpg",
                },
                new Tag()
                {
                    Id = 102,
                    ParentTagId = 4,
                    Name = "Chemia organiczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia_organiczna.jpg",
                },
                new Tag()
                {
                    Id = 103,
                    ParentTagId = 4,
                    Name = "Chemia analityczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia_analityczna.jpg",
                },
                new Tag()
                {
                    Id = 104,
                    ParentTagId = 4,
                    Name = "Chemia fizyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia.jpg",
                },
                new Tag()
                {
                    Id = 105,
                    ParentTagId = 4,
                    Name = "Chemia inorganiczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia.jpg",
                },
                new Tag()
                {
                    Id = 106,
                    ParentTagId = 4,
                    Name = "Chemia biologiczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chemia_biologiczna.jpg",
                },
                new Tag()
                {
                    Id = 107,
                    ParentTagId = 5,
                    Name = "Biologia molekularna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_molekularna.jpg",
                },
                new Tag()
                {
                    Id = 108,
                    ParentTagId = 5,
                    Name = "Biologia komórkowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_komórkowa.jpg",
                },
                new Tag()
                {
                    Id = 109,
                    ParentTagId = 5,
                    Name = "Biologia ewolucyjna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_ewolucyjna.jpg",
                },
                new Tag()
                {
                    Id = 110,
                    ParentTagId = 5,
                    Name = "Biologia systematyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia.jpg",
                },
                new Tag()
                {
                    Id = 111,
                    ParentTagId = 5,
                    Name = "Biologia populacyjna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia.jpg",
                },
                new Tag()
                {
                    Id = 112,
                    ParentTagId = 5,
                    Name = "Biologia ekologiczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_ekologiczna.jpg",
                },
                new Tag()
                {
                    Id = 113,
                    ParentTagId = 5,
                    Name = "Biologia roślin",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_roślin.jpg",
                },
                new Tag()
                {
                    Id = 114,
                    ParentTagId = 5,
                    Name = "Biologia zwierząt",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/biologia_zwierząt.jpg",
                },
                new Tag()
                {
                    Id = 115,
                    ParentTagId = 6,
                    Name = "Geomorfologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geomorfologia.jpg",
                },
                new Tag()
                {
                    Id = 116,
                    ParentTagId = 6,
                    Name = "Geologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geologia.jpg",
                },
                new Tag()
                {
                    Id = 117,
                    ParentTagId = 6,
                    Name = "Geografia fizyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia_fizyczna.jpg",
                },
                new Tag()
                {
                    Id = 118,
                    ParentTagId = 6,
                    Name = "Geografia polityczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg",
                },
                new Tag()
                {
                    Id = 119,
                    ParentTagId = 6,
                    Name = "Geografia ekonomiczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg",
                },
                new Tag()
                {
                    Id = 120,
                    ParentTagId = 6,
                    Name = "Geografia społeczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg",
                },
                new Tag()
                {
                    Id = 121,
                    ParentTagId = 6,
                    Name = "Geografia kulturowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia_kulturowa.jpg",
                },
                new Tag()
                {
                    Id = 122,
                    ParentTagId = 6,
                    Name = "Geografia regionalna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia_regionalna.jpg",
                },
                new Tag()
                {
                    Id = 123,
                    ParentTagId = 6,
                    Name = "Geografia turystyki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/geografia.jpg",
                },
                new Tag()
                {
                    Id = 124,
                    ParentTagId = 7,
                    Name = "Historia starożytna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_starożytna.jpg",
                },
                new Tag()
                {
                    Id = 125,
                    ParentTagId = 7,
                    Name = "Historia średniowiecza",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_średniowiecza.jpg",
                },
                new Tag()
                {
                    Id = 126,
                    ParentTagId = 7,
                    Name = "Historia nowożytna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_nowożytna.jpg",
                },
                new Tag()
                {
                    Id = 127,
                    ParentTagId = 7,
                    Name = "Historia współczesna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/historia_współczesna.jpg",
                },
                new Tag()
                {
                    Id = 128,
                    ParentTagId = 8,
                    Name = "Nauki społeczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_społeczne.jpg",
                },
                new Tag()
                {
                    Id = 129,
                    ParentTagId = 8,
                    Name = "Socjologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/socjologia.jpg",
                },
                new Tag()
                {
                    Id = 130,
                    ParentTagId = 8,
                    Name = "Antropologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/antropologia.jpg",
                },
                new Tag()
                {
                    Id = 131,
                    ParentTagId = 8,
                    Name = "Etnografia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/etnografia.jpg",
                },
                new Tag()
                {
                    Id = 132,
                    ParentTagId = 8,
                    Name = "Etnologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/etnologia.jpg",
                },
                new Tag()
                {
                    Id = 133,
                    ParentTagId = 8,
                    Name = "Etnomuzykologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/etnomuzykologia.jpg",
                },
                new Tag()
                {
                    Id = 134,
                    ParentTagId = 11,
                    Name = "Język polski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_polski.jpg",
                },
                new Tag()
                {
                    Id = 135,
                    ParentTagId = 11,
                    Name = "Język angielski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_angielski.jpg",
                },
                new Tag()
                {
                    Id = 136,
                    ParentTagId = 11,
                    Name = "Język niemiecki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_niemiecki.jpg",
                },
                new Tag()
                {
                    Id = 137,
                    ParentTagId = 11,
                    Name = "Język rosyjski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_rosyjski.jpg",
                },
                new Tag()
                {
                    Id = 138,
                    ParentTagId = 11,
                    Name = "Język francuski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_francuski.jpg",
                },
                new Tag()
                {
                    Id = 139,
                    ParentTagId = 11,
                    Name = "Język hiszpański",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_hiszpański.jpg",
                },
                new Tag()
                {
                    Id = 140,
                    ParentTagId = 11,
                    Name = "Język włoski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_włoski.jpg",
                },
                new Tag()
                {
                    Id = 141,
                    ParentTagId = 11,
                    Name = "Język japoński",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_japoński.jpg",
                },
                new Tag()
                {
                    Id = 142,
                    ParentTagId = 11,
                    Name = "Język chiński",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_chiński.jpg",
                },
                new Tag()
                {
                    Id = 143,
                    ParentTagId = 11,
                    Name = "Język koreański",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_koreański.jpg",
                },
                new Tag()
                {
                    Id = 144,
                    ParentTagId = 11,
                    Name = "Język portugalski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/język_portugalski.jpg",
                },
                new Tag()
                {
                    Id = 145,
                    ParentTagId = 9,
                    Name = "Psychologia poznawcza",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_poznawcza.jpg",
                },
                new Tag()
                {
                    Id = 146,
                    ParentTagId = 9,
                    Name = "Psychologia rozwojowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_rozwojowa.jpg",
                },
                new Tag()
                {
                    Id = 147,
                    ParentTagId = 9,
                    Name = "Psychologia społeczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg",
                },
                new Tag()
                {
                    Id = 148,
                    ParentTagId = 9,
                    Name = "Psychologia kliniczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_kliniczna.jpg",
                },
                new Tag()
                {
                    Id = 149,
                    ParentTagId = 9,
                    Name = "Psychologia pracy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_pracy.jpg",
                },
                new Tag()
                {
                    Id = 150,
                    ParentTagId = 9,
                    Name = "Psychologia edukacyjna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg",
                },
                new Tag()
                {
                    Id = 151,
                    ParentTagId = 9,
                    Name = "Psychologia sportu",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia_sportu.jpg",
                },
                new Tag()
                {
                    Id = 152,
                    ParentTagId = 9,
                    Name = "Psychologia kryminalna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg",
                },
                new Tag()
                {
                    Id = 153,
                    ParentTagId = 9,
                    Name = "Psychologia medyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/psychologia.jpg",
                },
                new Tag()
                {
                    Id = 154,
                    ParentTagId = 10,
                    Name = "Malarstwo",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/malarstwo.jpg",
                },
                new Tag()
                {
                    Id = 155,
                    ParentTagId = 10,
                    Name = "Rzeźba",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/rzeźba.jpg",
                },
                new Tag()
                {
                    Id = 156,
                    ParentTagId = 10,
                    Name = "Grafika",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/grafika.jpg",
                },
                new Tag()
                {
                    Id = 157,
                    ParentTagId = 10,
                    Name = "Fotografia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/fotografia.jpg",
                },
                new Tag()
                {
                    Id = 158,
                    ParentTagId = 10,
                    Name = "Film",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/film.jpg",
                },
                new Tag()
                {
                    Id = 159,
                    ParentTagId = 10,
                    Name = "Muzyka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/muzyka.jpg",
                },
                new Tag()
                {
                    Id = 160,
                    ParentTagId = 10,
                    Name = "Teatr",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teatr.jpg",
                },
                new Tag()
                {
                    Id = 161,
                    ParentTagId = 10,
                    Name = "Literatura",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/literatura.jpg",
                },
                new Tag()
                {
                    Id = 162,
                    ParentTagId = 12,
                    Name = "Prawo podatkowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 163,
                    ParentTagId = 12,
                    Name = "Prawo cywilne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 164,
                    ParentTagId = 12,
                    Name = "Prawo karne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 165,
                    ParentTagId = 12,
                    Name = "Prawo administracyjne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 166,
                    ParentTagId = 12,
                    Name = "Prawo pracy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 167,
                    ParentTagId = 12,
                    Name = "Prawo gospodarcze",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 168,
                    ParentTagId = 12,
                    Name = "Prawo handlowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 169,
                    ParentTagId = 13,
                    Name = "Prawo traktatów",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 170,
                    ParentTagId = 13,
                    Name = "Prawo dyplomatyczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 171,
                    ParentTagId = 13,
                    Name = "Prawo konsularne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 172,
                    ParentTagId = 13,
                    Name = "Prawo kosmiczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/prawo.jpg",
                },
                new Tag()
                {
                    Id = 173,
                    ParentTagId = 14,
                    Name = "Anatomia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/anatomia.jpg",
                },
                new Tag()
                {
                    Id = 174,
                    ParentTagId = 14,
                    Name = "Chirurgia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chirurgia.jpg",
                },
                new Tag()
                {
                    Id = 175,
                    ParentTagId = 14,
                    Name = "Farmakologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/farmakologia.jpg",
                },
                new Tag()
                {
                    Id = 176,
                    ParentTagId = 14,
                    Name = "Gastroenterologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/gastroenterologia.jpg",
                },
                new Tag()
                {
                    Id = 177,
                    ParentTagId = 14,
                    Name = "Ginekologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/ginekologia.jpg",
                },
                new Tag()
                {
                    Id = 178,
                    ParentTagId = 14,
                    Name = "Hematologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/hematologia.jpg",
                },
                new Tag()
                {
                    Id = 179,
                    ParentTagId = 14,
                    Name = "Kardiologia",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/kardiologia.jpg",
                },
                new Tag()
                {
                    Id = 180,
                    ParentTagId = 14,
                    Name = "Medycyna rodzinna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/medycyna_rodzinna.jpg",
                },
                new Tag()
                {
                    Id = 181,
                    ParentTagId = 14,
                    Name = "Medycyna pracy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/medycyna_pracy.jpg",
                },
                new Tag()
                {
                    Id = 182,
                    ParentTagId = 15,
                    Name = "Automatyka i robotyka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/automatyka_i_robotyka.jpg",
                },
                new Tag()
                {
                    Id = 183,
                    ParentTagId = 15,
                    Name = "Elektronika",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/elektronika.jpg",
                },
                new Tag()
                {
                    Id = 184,
                    ParentTagId = 15,
                    Name = "Elektrotechnika",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/elektrotechnika.jpg",
                },
                new Tag()
                {
                    Id = 185,
                    ParentTagId = 15,
                    Name = "Mechanika",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/mechanika.jpg",
                },
                new Tag()
                {
                    Id = 186,
                    ParentTagId = 15,
                    Name = "Nauki o Ziemi",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_o_ziemi.jpg",
                },
                new Tag()
                {
                    Id = 187,
                    ParentTagId = 15,
                    Name = "Nauki o materiałach",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/inżynieria.jpg",
                },
                new Tag()
                {
                    Id = 188,
                    ParentTagId = 15,
                    Name = "Nauki o transportie",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_o_transportie.jpg",
                },
                new Tag()
                {
                    Id = 189,
                    ParentTagId = 15,
                    Name = "Nauki o wodzie",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/nauki_o_wodzie.jpg",
                },
                new Tag()
                {
                    Id = 190,
                    ParentTagId = 15,
                    Name = "Nauki o zrównoważonym rozwoju",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/inżynieria.jpg",
                },
                new Tag()
                {
                    Id = 191,
                    ParentTagId = 16,
                    Name = "Kryptowaluty",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/kryptowaluty.jpg",
                },
                new Tag()
                {
                    Id = 192,
                    ParentTagId = 16,
                    Name = "Giełda",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/ekonomia.jpg",
                },
                new Tag()
                {
                    Id = 193,
                    ParentTagId = 16,
                    Name = "Finanse",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/finanse.jpg",
                },
                new Tag()
                {
                    Id = 194,
                    ParentTagId = 16,
                    Name = "Podatki",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/podatki.jpg",
                },
                new Tag()
                {
                    Id = 195,
                    ParentTagId = 17,
                    Name = "Polityka społeczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/polityka_społeczna.jpg",
                },
                new Tag()
                {
                    Id = 196,
                    ParentTagId = 17,
                    Name = "Polityka zagraniczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/polityka.jpg",
                },
                new Tag()
                {
                    Id = 197,
                    ParentTagId = 17,
                    Name = "Europeistyka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/europeistyka.jpg",
                },
                new Tag()
                {
                    Id = 198,
                    ParentTagId = 18,
                    Name = "Chrześcijaństwo",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/chrześcijaństwo.jpg",
                },
                new Tag()
                {
                    Id = 199,
                    ParentTagId = 18,
                    Name = "Islam",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/islam.jpg",
                },
                new Tag()
                {
                    Id = 200,
                    ParentTagId = 18,
                    Name = "Judaizm",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/judaizm.jpg",
                },
                new Tag()
                {
                    Id = 201,
                    ParentTagId = 18,
                    Name = "Buddyzm",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/buddyzm.jpg",
                },
                new Tag()
                {
                    Id = 202,
                    ParentTagId = 18,
                    Name = "Hinduizm",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/hinduizm.jpg",
                },
                new Tag()
                {
                    Id = 203,
                    ParentTagId = 18,
                    Name = "Bahaizm",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/teologia.jpg",
                },
                new Tag()
                {
                    Id = 204,
                    ParentTagId = 18,
                    Name = "Protestantyzm",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/protestantyzm.jpg",
                },
                new Tag()
                {
                    Id = 205,
                    ParentTagId = 18,
                    Name = "Świadkowie Jehowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/świadkowie_jehowy.jpg",
                },
                new Tag()
                {
                    Id = 206,
                    ParentTagId = 19,
                    Name = "Wychowanie wczesnoszkolne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_wczesnoszkolne.jpg",
                },
                new Tag()
                {
                    Id = 207,
                    ParentTagId = 19,
                    Name = "Wychowanie przedszkolne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_przedszkolne.jpg",
                },
                new Tag()
                {
                    Id = 208,
                    ParentTagId = 19,
                    Name = "Wychowanie szkolne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_szkolne.jpg",
                },
                new Tag()
                {
                    Id = 209,
                    ParentTagId = 19,
                    Name = "Wychowanie gimnazjalne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_gimnazjalne.jpg",
                },
                new Tag()
                {
                    Id = 210,
                    ParentTagId = 19,
                    Name = "Wychowanie licealne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_licealne.jpg",
                },
                new Tag()
                {
                    Id = 211,
                    ParentTagId = 19,
                    Name = "Wychowanie studenckie",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_studenckie.jpg",
                },
                new Tag()
                {
                    Id = 212,
                    ParentTagId = 19,
                    Name = "Wychowanie dorosłych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_dorosłych.jpg",
                },
                new Tag()
                {
                    Id = 213,
                    ParentTagId = 19,
                    Name = "Wychowanie specjalne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_specjalne.jpg",
                },
                new Tag()
                {
                    Id = 214,
                    ParentTagId = 19,
                    Name = "Wychowanie w rodzinie",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/wychowanie_w_rodzinie.jpg",
                },
                new Tag()
                {
                    Id = 215,
                    ParentTagId = 20,
                    Name = "Piłka nożna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/piłka_nożna.jpg",
                },
                new Tag()
                {
                    Id = 216,
                    ParentTagId = 20,
                    Name = "Koszykówka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/koszykówka.jpg",
                },
                new Tag()
                {
                    Id = 217,
                    ParentTagId = 20,
                    Name = "Siatkówka",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/siatkówka.jpg",
                },
                new Tag()
                {
                    Id = 218,
                    ParentTagId = 20,
                    Name = "Tenis",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/tenis.jpg",
                },
                new Tag()
                {
                    Id = 219,
                    ParentTagId = 20,
                    Name = "Siatkówka plażowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/siatkówka_plażowa.jpg",
                },
                new Tag()
                {
                    Id = 220,
                    ParentTagId = 20,
                    Name = "Piłka ręczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/piłka_ręczna.jpg",
                },
                new Tag()
                {
                    Id = 221,
                    ParentTagId = 20,
                    Name = "Piłka wodna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/piłka_wodna.jpg",
                },
                new Tag()
                {
                    Id = 222,
                    ParentTagId = 20,
                    Name = "Kolarstwo",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/kolarstwo.jpg",
                },
                new Tag()
                {
                    Id = 223,
                    ParentTagId = 20,
                    Name = "Bieganie",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/bieganie.jpg",
                },
                new Tag()
                {
                    Id = 224,
                    ParentTagId = 21,
                    Name = "Turystyka poznawczawa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_poznawczawa.jpgjpg",
                },
                new Tag()
                {
                    Id = 225,
                    ParentTagId = 21,
                    Name = "Turystyka rekreacyjna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_rekreacyjna.jpg",
                },
                new Tag()
                {
                    Id = 226,
                    ParentTagId = 21,
                    Name = "Turystyka sportowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_sportowa.jpg",
                },
                new Tag()
                {
                    Id = 227,
                    ParentTagId = 21,
                    Name = "Turystyka biznesowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg",
                },
                new Tag()
                {
                    Id = 228,
                    ParentTagId = 21,
                    Name = "Turystyka medyczna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg",
                },
                new Tag()
                {
                    Id = 229,
                    ParentTagId = 21,
                    Name = "Turystyka kulturowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka_kulturowa.jpg",
                },
                new Tag()
                {
                    Id = 230,
                    ParentTagId = 21,
                    Name = "Turystyka religijna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/turystyka.jpg",
                },
                new Tag()
                {
                    Id = 231,
                    ParentTagId = 22,
                    Name = "Transport lotniczy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_lotniczy.jpg",
                },
                new Tag()
                {
                    Id = 232,
                    ParentTagId = 22,
                    Name = "Transport drogowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_drogowy.jpg",
                },
                new Tag()
                {
                    Id = 233,
                    ParentTagId = 22,
                    Name = "Transport wodny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_wodny.jpg",
                },
                new Tag()
                {
                    Id = 234,
                    ParentTagId = 22,
                    Name = "Transport kolejowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_kolejowy.jpg",
                },
                new Tag()
                {
                    Id = 235,
                    ParentTagId = 22,
                    Name = "Transport morski",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/transport_morski.jpg",
                },
                new Tag()
                {
                    Id = 236,
                    ParentTagId = 22,
                    Name = "Planowanie trasy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/planowanie_trasy.jpg",
                },
                new Tag()
                {
                    Id = 237,
                    ParentTagId = 26,
                    Name = "Airbus A320",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/a320.jpg",
                },
                new Tag()
                {
                    Id = 238,
                    ParentTagId = 26,
                    Name = "Airbus A380",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/a380.jpg",
                },
                new Tag()
                {
                    Id = 239,
                    ParentTagId = 26,
                    Name = "Boeing 747",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_747.jpg",
                },
                new Tag()
                {
                    Id = 240,
                    ParentTagId = 26,
                    Name = "Boeing 777",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_777.jpg",
                },
                new Tag()
                {
                    Id = 241,
                    ParentTagId = 26,
                    Name = "Boeing 787",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_787.jpg",
                },
                new Tag()
                {
                    Id = 242,
                    ParentTagId = 26,
                    Name = "Boeing 737",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_737.jpg",
                },
                new Tag()
                {
                    Id = 243,
                    ParentTagId = 26,
                    Name = "Boeing 767",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/boeing_767.jpg",
                },
                new Tag()
                {
                    Id = 244,
                    ParentTagId = 26,
                    Name = "Airbus A350",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/a350.jpg",
                },
                new Tag()
                {
                    Id = 245,
                    ParentTagId = 23,
                    Name = "Reklama",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/reklama.jpg",
                },
                new Tag()
                {
                    Id = 246,
                    ParentTagId = 23,
                    Name = "PR",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 247,
                    ParentTagId = 23,
                    Name = "Social Media",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/social_media.jpg",
                },
                new Tag()
                {
                    Id = 248,
                    ParentTagId = 23,
                    Name = "E-mail marketing",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 249,
                    ParentTagId = 23,
                    Name = "Marketing wizualny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 250,
                    ParentTagId = 23,
                    Name = "Marketing wewnętrzny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 251,
                    ParentTagId = 23,
                    Name = "Marketing internetowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 252,
                    ParentTagId = 23,
                    Name = "Marketing telewizyjny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing_telewizyjny.jpg",
                },
                new Tag()
                {
                    Id = 253,
                    ParentTagId = 23,
                    Name = "Marketing mobilny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing_mobilny.jpg",
                },
                new Tag()
                {
                    Id = 254,
                    ParentTagId = 23,
                    Name = "Marketing bezpośredni",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 255,
                    ParentTagId = 23,
                    Name = "Marketing relacji publicznych",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 256,
                    ParentTagId = 23,
                    Name = "Marketing strategiczny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 257,
                    ParentTagId = 23,
                    Name = "Marketing operacyjny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 258,
                    ParentTagId = 23,
                    Name = "Marketing terytorialny",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 259,
                    ParentTagId = 23,
                    Name = "Marketing usługowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 260,
                    ParentTagId = 23,
                    Name = "Marketing produktowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 261,
                    ParentTagId = 23,
                    Name = "Marketing segmentowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 262,
                    ParentTagId = 23,
                    Name = "Marketing kanałowy",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/marketing.jpg",
                },
                new Tag()
                {
                    Id = 263,
                    ParentTagId = 24,
                    Name = "Dziennikrastwo śledcze",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_sledcze.jpg",
                },
                new Tag()
                {
                    Id = 264,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo internetowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_internetowe.jpg",
                },
                new Tag()
                {
                    Id = 265,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo lokalne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_lokalne.jpg",
                },
                new Tag()
                {
                    Id = 266,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo międzynarodowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg",
                },
                new Tag()
                {
                    Id = 267,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo prasowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg",
                },
                new Tag()
                {
                    Id = 268,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo publicystyczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg",
                },
                new Tag()
                {
                    Id = 269,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo radiowe",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_radiowe.jpg",
                },
                new Tag()
                {
                    Id = 270,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo telewizyjne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_telewizyjne.jpg",
                },
                new Tag()
                {
                    Id = 271,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo wideo",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo.jpg",
                },
                new Tag()
                {
                    Id = 272,
                    ParentTagId = 24,
                    Name = "Dziennikarstwo społeczne",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/dziennikarstwo_spoleczne.jpg",
                },
                new Tag()
                {
                    Id = 273,
                    ParentTagId = 25,
                    Name = "Architektura mieszkalna",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_mieszkalna.jpg",
                },
                new Tag()
                {
                    Id = 274,
                    ParentTagId = 25,
                    Name = "Architektura wnętrz",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_wnetrz.jpg",
                },
                new Tag()
                {
                    Id = 275,
                    ParentTagId = 25,
                    Name = "Architektura krajobrazu",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_krajobrazu.jpg",
                },
                new Tag()
                {
                    Id = 276,
                    ParentTagId = 25,
                    Name = "Architektura przestrzeni publicznej",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_przestrzeni_publicznej.jpg",
                },
                new Tag()
                {
                    Id = 277,
                    ParentTagId = 25,
                    Name = "Architektura przemysłowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_przemyslowa.jpg",
                },
                new Tag()
                {
                    Id = 278,
                    ParentTagId = 25,
                    Name = "Architektura ogrodowa",
                    ImageUrl = "https://dev.uniquanda.pl:2002/api/Image/Tags/architektura_ogrodowa.jpg",
                }

            );
        }
    }
}