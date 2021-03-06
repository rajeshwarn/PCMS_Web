﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Receptionist/receptionMaster.Master" AutoEventWireup="true" CodeBehind="oldPatients.aspx.cs" Inherits="PCMS_Web.Receptionist.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        
    <div class="col-sm-12">
         <div class="alert alert-danger" role="alert" runat="server" visible="false" id="errorMsg_alert">
             <asp:Label ID="error_lb" runat="server" Text="Label" ></asp:Label>
         </div>
        <script type="text/javascript" src="http://code.jquery.com/jquery-latest.js">
             $(document).ready(function () {
                    $('#errorMsg_alert').delay(2000).fadeOut();
                });
         </script>      
        <div class="box box-info">
            <div class="box-header with-border">
              <h3 class="box-title">Old Patients</h3>
            </div>
        <div class="box-body">
            <div class="row">
                <div class="col-sm-4 col-sm-offset-6">
                    <asp:TextBox ID="searchPatient_txt" runat="server" CssClass="form-control" placeholder="Enter Patient Name Here"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:Button ID="searchPatient_btn" runat="server" Text="Search" CssClass="btn btn-info" OnClick="searchPatient_btn_Click" />
                </div>
            </div><br />

            <div class="table-responsive">
                <div class="col-sm-12">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table" DataSourceID="PatientDataSource" DataKeyNames="patient_reg" AllowPaging="True" AllowSorting="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="patient_reg" HeaderText="ID" SortExpression="patient_reg" ReadOnly="True"></asp:BoundField>
                            <asp:BoundField DataField="full_name" HeaderText="Name" SortExpression="full_name"></asp:BoundField>
                            <asp:BoundField DataField="father_name" HeaderText="Father Name" SortExpression="father_name"></asp:BoundField>
                            <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address"></asp:BoundField>
                            <asp:BoundField DataField="mob" HeaderText="Mobile Number" SortExpression="mob"></asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email"></asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066"></FooterStyle>

                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"></HeaderStyle>

                        <PagerStyle HorizontalAlign="Left" BackColor="White" ForeColor="#000066"></PagerStyle>

                        <RowStyle ForeColor="#000066"></RowStyle>

                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                        <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                        <SortedAscendingHeaderStyle BackColor="#007DBB"></SortedAscendingHeaderStyle>

                        <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                        <SortedDescendingHeaderStyle BackColor="#00547E"></SortedDescendingHeaderStyle>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="PatientDataSource" ConnectionString='<%$ ConnectionStrings:doctorConnectionString %>' SelectCommand="SELECT * FROM [patient_registeration]" DeleteCommand="DELETE FROM [patient_registeration] WHERE [patient_reg] = @patient_reg" InsertCommand="INSERT INTO [patient_registeration] ([patient_reg], [cnic], [full_name], [father_name], [sex], [address], [city_id], [mob], [email], [attendant_name], [relation_id], [attendant_mob], [dob], [ref_doctor_id], [martial_status], [education], [occupation_id], [source]) VALUES (@patient_reg, @cnic, @full_name, @father_name, @sex, @address, @city_id, @mob, @email, @attendant_name, @relation_id, @attendant_mob, @dob, @ref_doctor_id, @martial_status, @education, @occupation_id, @source)" UpdateCommand="UPDATE [patient_registeration] SET [cnic] = @cnic, [full_name] = @full_name, [father_name] = @father_name, [sex] = @sex, [address] = @address, [city_id] = @city_id, [mob] = @mob, [email] = @email, [attendant_name] = @attendant_name, [relation_id] = @relation_id, [attendant_mob] = @attendant_mob, [dob] = @dob, [ref_doctor_id] = @ref_doctor_id, [martial_status] = @martial_status, [education] = @education, [occupation_id] = @occupation_id, [source] = @source WHERE [patient_reg] = @patient_reg">
                        <DeleteParameters>
                            <asp:Parameter Name="patient_reg" Type="Int32"></asp:Parameter>
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="patient_reg" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="cnic" Type="String"></asp:Parameter>
                            <asp:Parameter Name="full_name" Type="String"></asp:Parameter>
                            <asp:Parameter Name="father_name" Type="String"></asp:Parameter>
                            <asp:Parameter Name="sex" Type="Byte"></asp:Parameter>
                            <asp:Parameter Name="address" Type="String"></asp:Parameter>
                            <asp:Parameter Name="city_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="mob" Type="String"></asp:Parameter>
                            <asp:Parameter Name="email" Type="String"></asp:Parameter>
                            <asp:Parameter Name="attendant_name" Type="String"></asp:Parameter>
                            <asp:Parameter Name="relation_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="attendant_mob" Type="String"></asp:Parameter>
                            <asp:Parameter DbType="Date" Name="dob"></asp:Parameter>
                            <asp:Parameter Name="ref_doctor_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="martial_status" Type="String"></asp:Parameter>
                            <asp:Parameter Name="education" Type="String"></asp:Parameter>
                            <asp:Parameter Name="occupation_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="source" Type="Byte"></asp:Parameter>
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="cnic" Type="String"></asp:Parameter>
                            <asp:Parameter Name="full_name" Type="String"></asp:Parameter>
                            <asp:Parameter Name="father_name" Type="String"></asp:Parameter>
                            <asp:Parameter Name="sex" Type="Byte"></asp:Parameter>
                            <asp:Parameter Name="address" Type="String"></asp:Parameter>
                            <asp:Parameter Name="city_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="mob" Type="String"></asp:Parameter>
                            <asp:Parameter Name="email" Type="String"></asp:Parameter>
                            <asp:Parameter Name="attendant_name" Type="String"></asp:Parameter>
                            <asp:Parameter Name="relation_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="attendant_mob" Type="String"></asp:Parameter>
                            <asp:Parameter DbType="Date" Name="dob"></asp:Parameter>
                            <asp:Parameter Name="ref_doctor_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="martial_status" Type="String"></asp:Parameter>
                            <asp:Parameter Name="education" Type="String"></asp:Parameter>
                            <asp:Parameter Name="occupation_id" Type="Int32"></asp:Parameter>
                            <asp:Parameter Name="source" Type="Byte"></asp:Parameter>
                            <asp:Parameter Name="patient_reg" Type="Int32"></asp:Parameter>
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="PatientSearchDataSource" runat="server" ConnectionString='<%$ ConnectionStrings:doctorConnectionString %>' SelectCommand="SELECT * FROM [patient_registeration] WHERE ([full_name] LIKE '%' + @full_name + '%')">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="searchPatient_txt" PropertyName="Text" Name="full_name" Type="String"></asp:ControlParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
                
            </div>
            <hr/>
                <div class="row">
                    <h4 class="col-sm-4 control-label">Selected Patient</h4>
                </div>
                  <br />
                <div class="row">
                    <div class="form-group col-sm-6">
                      <label class="col-sm-4 control-label">Patient ID</label>

                      <div class="col-sm-8">
                        <asp:TextBox ID="patientId_txt" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                    </div>
                    <div class="form-group col-sm-6">
                      <label class="col-sm-4 control-label">Full Name</label>

                      <div class="col-sm-8">
                        <asp:TextBox ID="fullName_txt"  Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                    </div>
                </div>            
            <div class="row">
                 <div class="form-group col-sm-6">
                         <label class="col-sm-4 control-label">Fee</label>

                        <div class="col-sm-8">
                            <asp:TextBox ID="fee" CssClass="form-control" Required="" Title="Enter valid number"  Pattern="^[0-9]{1,10}$"  runat="server"></asp:TextBox>
                        </div>
                    </div>
                <div class="form-group col-sm-6">
                    <label class="col-sm-4 control-label">Assign Doctor</label>

                        <div class="col-sm-8">
                            <asp:DropDownList ID="assignDoctor_dd" CssClass="form-control" runat="server" DataSourceID="AssignDoctorDataSource" DataTextField="full_name" DataValueField="employee_id"></asp:DropDownList>
                            <asp:SqlDataSource runat="server" ID="AssignDoctorDataSource" ConnectionString='<%$ ConnectionStrings:doctorConnectionString %>' SelectCommand="SELECT employee_id, full_name FROM user_registeration WHERE (user_type = 'Doctor')"></asp:SqlDataSource>
                    </div>
                </div>  
            </div>    
            <br/>
            </div>
            
        <div class="box-footer">
            <asp:Button ID="printReceipt_btn" runat="server" Text="Print" CssClass="btn btn-info pull-right" OnClick="printReceipt_btn_Click"/>
        </div>
            </div>
        </div>
    </form>
    
</asp:Content>