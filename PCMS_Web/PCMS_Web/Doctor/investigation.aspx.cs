﻿using PCMS_Web.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PCMS_Web.Doctor
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        string constring = ConfigurationManager.ConnectionStrings["PCMS_ConnectionString"].ConnectionString;
        string id = "";
        getInformation info = new getInformation();
        string[] patientInfo = new string[4];
        maxValue obj1 = new maxValue();
        int maxvisit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PatientId"] != null)
            {
                id = Session["PatientId"].ToString();
                patientInfo = info.information("SELECT a.full_name, a.father_name,a.dob, b.visit_no FROM patient_registeration a, visit b  WHERE a.patient_reg = b.patient_reg  AND b.visit_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'And  a.patient_reg ='" + Session["PatientId"].ToString() + "' And b.patient_reg='" + Session["PatientId"].ToString() + "';");
                patientId_txt.Text = Session["PatientId"].ToString();

                visitNumber_txt.Text = patientInfo[2];
                patientName.Text = patientInfo[0];
                ageTxt.Text = patientInfo[3];
                if (!Page.IsPostBack)
                {
                    GetHistory();

                }
            }
        }

        public void GetHistory()
        {

            maxvisit = obj1.max("Select max(visit_no) from investigation where id='" + patientId_txt.Text + "';");
            if (maxvisit == Convert.ToInt32(visitNumber_txt.Text))
            {
                submit_btn.Visible = false;
                update_btn.Visible = true;
            }
            else
            {
                update_btn.Visible = false;
                submit_btn.Visible = true;
            }
            if (maxvisit > 0)
            {
                try
                {
                    patientId_txt.Text = id;
                    SqlConnection con = new SqlConnection(constring);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM investigation where id='" + id + "' and visit_no='"+ maxvisit + "'"  , con);
                    cmd.Connection = con;
                    SqlDataReader dr;
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        completeBloodExaminationComments_area.InnerText = dr["completeBloodExamination"].ToString();
                        completeUrineExaminationComments_area.InnerText = dr["completeUrineExamination"].ToString();
                        liverTestComments_area.InnerText = dr["liverFunctionTest"].ToString();
                        ecgComments_area.InnerText = dr["ecg"].ToString();

                        if ((Convert.ToInt32(dr["bloodSugarRb"].ToString()) == 1))
                        {
                            fasting_radio.Checked = true;
                            random_radio.Checked = false;
                        }
                        else
                        {
                            random_radio.Checked = true;
                        }

                        bloodSugarComments_area.InnerText = dr["bloodSugarArea"].ToString();
                        thyroidFunctionComments_area.InnerText = dr["thyroidFuctionTest"].ToString();
                        radiographyComments_area.InnerText = dr["radiography"].ToString();
                        ulgrasonographyComments_area.InnerText = dr["ulgranSonography"].ToString();
                        ctScanComments_area.InnerText = dr["ctScan"].ToString();
                        otherComments_area.InnerText = dr["otherComments"].ToString();

                        if (Convert.ToInt32(dr["completeBloodRadio"]) == 1)
                            yesCompleteBloodExamination_radio.Checked = true;
                        else
                            noCompleteBloodExamination_radio.Checked = true;

                        if (Convert.ToInt32(dr["completeUrineRadio"]) == 1)
                            yesCompleteUrineExamination_radio.Checked = true;
                        else
                            noCompleteUrineExamination_radio.Checked = true;

                        if (Convert.ToInt32(dr["liverFunctionRadio"]) == 1)
                            yesLiverFunction_radio.Checked = true;
                        else
                            noLiverFuntion_radio.Checked = true;

                        if (Convert.ToInt32(dr["ecgRadio"]) == 1)
                            yesECG_radio.Checked = true;
                        else
                            noECG_radio.Checked = true;

                        if (Convert.ToInt32(dr["bloodSugarRadio"]) == 1)
                            yesBloodSugar_radio.Checked = true;
                        else
                            noBloodSugar_radio.Checked = true;

                        if (Convert.ToInt32(dr["thyroidFunctionRadio"]) == 1)
                            yesThyroid_radio.Checked = true;
                        else
                            noThyroid_radio.Checked = true;

                        if (Convert.ToInt32(dr["radiographyRadio"]) == 1)
                            yesRadiography_radio.Checked = true;
                        else
                            noRadiography_radio.Checked = true;

                        if (Convert.ToInt32(dr["ulgraRadio"]) == 1)
                            yesUlgra_radio.Checked = true;
                        else
                            noUlgra_radio.Checked = true;

                        if (Convert.ToInt32(dr["ctScanRadio"]) == 1)
                            yesCT_radio.Checked = true;
                        else
                            noCT_radio.Checked = true;

                    }
                    else
                    {
                        completeBloodExaminationComments_area.InnerText = "";
                        completeUrineExaminationComments_area.InnerText = "";
                        liverTestComments_area.InnerText = "";
                        ecgComments_area.InnerText = "";
                        bloodSugarComments_area.InnerText = "";
                        thyroidFunctionComments_area.InnerText = "";
                        radiographyComments_area.InnerText = "";
                        ulgrasonographyComments_area.InnerText = "";
                        ctScanComments_area.InnerText = "";
                        otherComments_area.InnerText = "";
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    alert_fail.Visible = true;
                    error.Text = "Error! " + ex.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
        }

        protected void submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("insert into investigation ( id,visit_no ,completeBloodExamination ,completeUrineExamination ,liverFunctionTest ,ecg ,bloodSugarRb ,bloodSugarArea ,thyroidFuctionTest ,radiography ,ulgranSonography ,ctScan ,otherComments, completeBloodRadio, completeUrineRadio, liverFunctionRadio, ecgRadio, bloodSugarRadio, thyroidFunctionRadio, radiographyRadio, ulgraRadio, ctScanRadio) values(@id,@visit_no ,@completeBloodExamination ,@completeUrineExamination ,@liverFunctionTest ,@ecg ,@bloodSugarRb ,@bloodSugarArea ,@thyroidFuctionTest ,@radiography ,@ulgranSonography ,@ctScan ,@otherComments,@completeBloodRadio, @completeUrineRadio, @liverFunctionRadio, @ecgRadio, @bloodSugarRadio, @thyroidFunctionRadio, @radiographyRadio, @ulgraRadio, @ctScanRadio)", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@visit_no", visitNumber_txt.Text);
                cmd.Parameters.AddWithValue("@completeBloodExamination", completeBloodExaminationComments_area.InnerText);
                cmd.Parameters.AddWithValue("@completeUrineExamination", completeUrineExaminationComments_area.InnerText);
                cmd.Parameters.AddWithValue("@liverFunctionTest", liverTestComments_area.InnerText);
                cmd.Parameters.AddWithValue("@ecg", ecgComments_area.InnerText);
                if (random_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRb", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRb", 1);
                }
                cmd.Parameters.AddWithValue("@bloodSugarArea", bloodSugarComments_area.InnerText);
                cmd.Parameters.AddWithValue("@thyroidFuctionTest", thyroidFunctionComments_area.InnerText);
                cmd.Parameters.AddWithValue("@radiography", radiographyComments_area.InnerText);
                cmd.Parameters.AddWithValue("@ulgranSonography", ulgrasonographyComments_area.InnerText);
                cmd.Parameters.AddWithValue("@ctScan", ctScanComments_area.InnerText);
                cmd.Parameters.AddWithValue("@otherComments", otherComments_area.InnerText);

                if (yesCompleteBloodExamination_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@completeBloodRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@completeBloodRadio", 2);
                }
                if (yesCompleteUrineExamination_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@completeUrineRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@completeUrineRadio", 2);
                }
                if (yesLiverFunction_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@liverFunctionRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@liverFunctionRadio", 2);
                }
                if (yesECG_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ecgRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ecgRadio", 2);
                }
                if (yesBloodSugar_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRadio", 2);
                }
                if (yesThyroid_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@thyroidFunctionRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@thyroidFunctionRadio", 2);
                }
                if (yesRadiography_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@radiographyRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@radiographyRadio", 2);
                }
                if (yesUlgra_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ulgraRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ulgraRadio", 2);
                }
                if (yesCT_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ctScanRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ctScanRadio", 2);
                }


                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                alert_success.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

            }
            catch (Exception ex)
            {
                alert_fail.Visible = true;
                error.Text = "Error! " + ex.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
        }

        protected void update_btn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(constring);
                SqlCommand cmd = new SqlCommand("update  investigation set completeBloodExamination=@completeBloodExamination, completeUrineExamination=@completeUrineExamination, liverFunctionTest=@liverFunctionTest, ecg=@ecg, bloodSugarRb=@bloodSugarRb, bloodSugarArea=@bloodSugarArea, thyroidFuctionTest=@thyroidFuctionTest, radiography=@radiography, ulgranSonography=@ulgranSonography, ctScan=@ctScan, otherComments=@otherComments, completeBloodRadio=@completeBloodRadio, completeUrineRadio=@completeUrineRadio, liverFunctionRadio=@liverFunctionRadio, ecgRadio=@ecgRadio, bloodSugarRadio=@bloodSugarRadio, thyroidFunctionRadio=@thyroidFunctionRadio, radiographyRadio=@radiographyRadio, ulgraRadio=@ulgraRadio, ctScanRadio=@ctScanRadio where id='" + id + "' and visit_no=@visit_no ", con);
                cmd.Parameters.AddWithValue("@visit_no", visitNumber_txt.Text);
                cmd.Parameters.AddWithValue("@completeBloodExamination", completeBloodExaminationComments_area.InnerText);
                cmd.Parameters.AddWithValue("@completeUrineExamination", completeUrineExaminationComments_area.InnerText);
                cmd.Parameters.AddWithValue("@liverFunctionTest", liverTestComments_area.InnerText);
                cmd.Parameters.AddWithValue("@ecg", ecgComments_area.InnerText);
                if (random_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRb", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRb", 1);
                }
                cmd.Parameters.AddWithValue("@bloodSugarArea", bloodSugarComments_area.InnerText);
                cmd.Parameters.AddWithValue("@thyroidFuctionTest", thyroidFunctionComments_area.InnerText);
                cmd.Parameters.AddWithValue("@radiography", radiographyComments_area.InnerText);
                cmd.Parameters.AddWithValue("@ulgranSonography", ulgrasonographyComments_area.InnerText);
                cmd.Parameters.AddWithValue("@ctScan", ctScanComments_area.InnerText);
                cmd.Parameters.AddWithValue("@otherComments", otherComments_area.InnerText);

                if (yesCompleteBloodExamination_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@completeBloodRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@completeBloodRadio", 2);
                }
                if (yesCompleteUrineExamination_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@completeUrineRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@completeUrineRadio", 2);
                }
                if (yesLiverFunction_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@liverFunctionRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@liverFunctionRadio", 2);
                }
                if (yesECG_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ecgRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ecgRadio", 2);
                }
                if (yesBloodSugar_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@bloodSugarRadio", 2);
                }
                if (yesThyroid_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@thyroidFunctionRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@thyroidFunctionRadio", 2);
                }
                if (yesRadiography_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@radiographyRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@radiographyRadio", 2);
                }
                if (yesUlgra_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ulgraRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ulgraRadio", 2);
                }
                if (yesCT_radio.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ctScanRadio", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ctScanRadio", 2);
                }


                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                alert_success.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
            catch (Exception ex)
            {
                alert_fail.Visible = true;
                error.Text = "Error! " + ex.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
        }
    }
}