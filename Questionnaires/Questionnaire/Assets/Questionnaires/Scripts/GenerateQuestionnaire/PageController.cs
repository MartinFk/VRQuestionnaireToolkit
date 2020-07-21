using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PageController.class
/// 
/// version 1.0
/// date: July 1st, 2020
/// authors: Martin Feick & Niko Kleer
/// </summary>

namespace VRQuestionnaireToolkit
{
    public class PageController : MonoBehaviour
    {
        private GameObject _vrQuestionnaireFactory;
        private PageFactory _pageFactory;
        private GameObject _export;

        void Start()
        {
            //init necessary relationships
            _vrQuestionnaireFactory = GameObject.FindGameObjectWithTag("QuestionnaireFactory");
            _export = GameObject.FindGameObjectWithTag("ExportToCSV");
            _pageFactory = _vrQuestionnaireFactory.GetComponent<PageFactory>();
        }

        public bool CheckMandatoryQuestionsAnswered()
        {
            int countMandatory = 0;
            int answeredMandatory = 0;

            for (int i = 0; i < _pageFactory.QuestionList.Count; i++)
            {
                if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>() != null)
                {
                    if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>()
                        .QMandatory)
                    {
                        countMandatory++;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Radio>()
                                .RadioList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                            {
                                answeredMandatory++;
                            }
                        }

                    }
                }
                else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>() != null)
                {
                    if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<RadioGrid>()
                        .QMandatory)
                    {
                        countMandatory++;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0]
                                .GetComponentInParent<RadioGrid>()
                                .RadioList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>()
                                .isOn)
                            {
                                answeredMandatory++;
                            }
                        }
                    }
                }
                else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>() != null)
                {
                    if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>() != null)
                    {
                        if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>()
                            .QMandatory)
                        {
                            countMandatory++;

                            for (int j = 0;
                                j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<Checkbox>()
                                    .CheckboxList.Count;
                                j++)
                            {
                                if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                                {
                                    answeredMandatory++;
                                }
                            }

                        }
                    }
                }
                else if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>() != null)
                {
                    if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>()
                        .QMandatory)
                    {
                        countMandatory++;

                        for (int j = 0;
                            j < _pageFactory.GetComponent<PageFactory>().QuestionList[i][0].GetComponentInParent<LinearGrid>()
                                .LinearGridList.Count;
                            j++)
                        {
                            if (_pageFactory.GetComponent<PageFactory>().QuestionList[i][j].GetComponentInChildren<Toggle>().isOn)
                            {
                                answeredMandatory++;
                            }
                        }

                    }
                }
            }

            if (countMandatory - answeredMandatory == 0)
                return true;
            else
                return false;
        }

        /*
         * GoToNextPage method
         *
         * Disables current page and enables next page
         * If the user reaches the final page the "Next" button becomes the "Submit" button with the underlying functionality
         * to call the Save() and Export() Method
         * Verifies that all "mandatory" questions on this page have been answered
         */
        public void GoToNextPage()
        {
            if (CheckMandatoryQuestionsAnswered() || _pageFactory.CurrentPage == 0)
            {
                // _pageFactory.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
                _pageFactory.PageList[_pageFactory.CurrentPage].SetActive(false);
                ++_pageFactory.CurrentPage;
                _pageFactory.PageList[_pageFactory.CurrentPage].SetActive(true);

                //reached second-last page
                if (_pageFactory.PageList.Count - 2 == _pageFactory.CurrentPage)
                {
                    GameObject q_footer = GameObject.Find("Q_Footer");
                    TextMeshProUGUI nextButton = q_footer.GetComponentInChildren<TextMeshProUGUI>();
                    nextButton.text = "Submit";
                }

                if (_pageFactory.PageList.Count - 1 == _pageFactory.CurrentPage)
                {
                    _export.GetComponent<ExportToCSV>().Save();
                }
            }
            else
            {
                //   _pageFactory.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(true);
            }
        }

        /*
         * GoToPreviousPage method
         *
         * Disables current page and enables previous page
         */
        public void GoToPreviousPage()
        {
            _pageFactory.PageList[_pageFactory.CurrentPage].SetActive(false);
            --_pageFactory.CurrentPage;
            _pageFactory.PageList[_pageFactory.CurrentPage].SetActive(true);
        }
    }
}