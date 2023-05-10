using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Core
{
    [System.Serializable]
    public enum ItemType
    {
        L�pis,
        Caneta,
        Caderno,
        Mochila,
        Garrafa
    }


    [System.Serializable]
    public enum Shifts
    {
        Manh�, Tarde, Noite
    }

    [System.Serializable]
    public enum Courses
    {
        Administra��o,
        Agronomia,
        Arquitetura,
        Artes_Visuais,
        Ci�ncia_da_Computa��o,
        Ci�ncias_Biol�gicas,
        Ci�ncias_Cont�beis,
        Ci�ncias_Econ�micas,
        Ci�ncias_Sociais,
        Audiovisual,
        Publicidade_e_Propaganda,
        Design,
        Direito,
        Ecologia,
        Educa��o_F�sica,
        Engenharia_Ambiental,
        Engenharia_Civ�l,
        Engenharia_de_Alimentos,
        Engenharia_de_Computa��o,
        Engenharia_de_Materiais,
        Engenharia_de_Produ��o,
        Engenharia_de_Software,
        Engenharia_El�trica,
        Engenharia_Mecatr�nica,
        Estat�stica,
        Filosifia,
        F�sica,
        Fisioterapia,
        Geografia,
        Geologia,
        Hist�ria,
        Jornalismo,
        Letras,
        Matem�tica,
        Meteorologia,
        M�sica,
        Nutri��o,
        Pedagogia,
        Qu�mica,
        Servi�o_Social,
        Teatro,
        Turismo,
        Zootecnia
    }

    [System.Serializable]
    public enum Period
    {
        Primeiro, Segundo, Terceiro, Quarto
    }

    [System.Serializable]
    public class Item
    {
        [SerializeField] ItemType itemType;
        [SerializeField] Shifts shift;
        [SerializeField] Courses course;
        [SerializeField] Period period;

        [Header("Informa��es")]
        [SerializeField] Registration ownerRegister;
        [SerializeField] string description;
        #region Getters
        public ItemType ItemType => itemType;
        public Shifts Shift => shift;
        public Courses Course => course;
        public Period Period => period;
        public Registration Register => ownerRegister;

        public string Description => description;

        #endregion

        public void Setup(ItemType _itemType, Shifts _shift, Courses _course, Period _period, Registration _reg = null)
        {
            ownerRegister ??= new();
            itemType = _itemType;
            shift = _shift;
            course = _course;
            period = _period;
            SetDescription();
            SetRegistration();
        }

        void SetDescription()
        {
            string name = "";
            course.ToString().Split('_').ToList().ForEach(x => name += x + " ");
            description = $"{itemType} � um item que foi encontrado depois de uma aula do curso de {name}" +
                $"de {period} per�odo no turno da {shift}";
        }

        void SetRegistration(Registration _reg = null)
        {
            if (_reg == null)
            {
                ownerRegister.CreateRegistry(shift, Course, Period);
            }
            else
            {
                ownerRegister = _reg;
            }
            
        }
    }

    [System.Serializable]
    public class Registration
    {
        [SerializeField] string registry;

        #region Getters
        public string Registry => registry;
        #endregion

        public void CreateRegistry(Shifts _shift, Courses _course, Period _period)
        {
            string shiftChunk = $"{(int)_shift + 1}";
            string courseChunk = $"0{((int)_course + 1).ToString("00")}";
            string registryNumber = $"0{(Random.Range(0, 40) + 1).ToString("00")}";
            registry = $"{System.DateTime.Now.Year - (int)_period}{courseChunk}{shiftChunk}{registryNumber}";
        }
    }
}