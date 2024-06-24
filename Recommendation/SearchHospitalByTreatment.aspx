<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="SearchHospitalByTreatment.aspx.cs" Inherits="Recommendation_SearchHospitalByTreatment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

     <style type="text/css">
        .Star {
            background-image: url(../images/Star.gif);
            height: 17px;
            width: 17px;
        }

        .WaitingStar {
            background-image: url(../images/WaitingStar.gif);
            height: 17px;
            width: 17px;
        }

        .FilledStar {
            background-image: url(../images/FilledStar.gif);
            height: 17px;
            width: 17px;
        }
    </style>

    <div class="col-md-12" style="height: 100px">
        <div class="block">
            <div class="block-title">
                <h2>Search Hospital</h2>
                <span style="float: right;">
                    <asp:HiddenField ID="HfId" runat="server" />
                    <asp:Label ID="lblmsg" runat="server" CssClass="label-display"></asp:Label>
                </span>
            </div>
            <div class="form-horizontal form-bordered">
                <div class="form-group" style="border-bottom: none;">

                    <div class="col-md-12">
                        <label class="control-label col-md-2">
                            Filter By
                        </label>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkCost" Text="Cost" runat="server" AutoPostBack="True" OnCheckedChanged="chkCost_CheckedChanged" />
                            &nbsp;

                                           <asp:CheckBox ID="chkFacility" Text="Facility" runat="server" AutoPostBack="True" OnCheckedChanged="chkFacility_CheckedChanged" />
                             &nbsp;
                                           <asp:CheckBox ID="chkRating" Text="Rating" runat="server" AutoPostBack="True" OnCheckedChanged="chkRating_CheckedChanged" />
                               &nbsp;
                                           <asp:CheckBox ID="chkDoctor" Text="Doctor" runat="server" AutoPostBack="True" OnCheckedChanged="chkDoctor_CheckedChanged" />
                             &nbsp;
                                           <asp:CheckBox ID="chkLocation" Text="Location" runat="server" AutoPostBack="True" OnCheckedChanged="chkLocation_CheckedChanged1" />
                        </div>
                    </div>

                    <div class="col-md-12" runat="server" id="divDisease" visible="true">
                        <label class="control-label col-md-2">
                            Select Disease
                        </label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlDisease" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDisease_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="ddlDisease" ForeColor="Red" InitialValue="0">Please Select Disease</asp:RequiredFieldValidator>
                        </div>
                    </div>

                  
                    <div class="col-md-12" runat="server">
                        <label class="control-label col-md-2">
                            Select Treatment
                        </label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlTreatment" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTreatment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="ddlTreatment" ForeColor="Red" InitialValue="0">Please Select Treatment</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-12" runat="server" id="divCost" visible="false">
                        <label class="control-label col-md-2">
                            Select Treatment Cost Range
                        </label>
                        <div class="col-md-3">
                            <asp:ListBox ID="costList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="costList_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="All"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Less than 5000"></asp:ListItem>
                                <asp:ListItem Value="3" Text="5000 To 20000"></asp:ListItem>
                                <asp:ListItem Value="4" Text="20000 To 50000"></asp:ListItem>
                                <asp:ListItem Value="5" Text="50000 To 100000"></asp:ListItem>
                                <asp:ListItem Value="6" Text="More than 100000"></asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>

                    <div class="col-md-12" runat="server" id="divFacility" visible="false">
                        <label class="control-label col-md-2">
                            Select Facility
                        </label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlFacility" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFacility_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFacility" ForeColor="Red" InitialValue="0">Please Select Facility</asp:RequiredFieldValidator>
                        </div>
                    </div>
                      <div style="clear: both; height: 10px;">
                    </div>
                      <div class="col-md-12" runat="server"  id="divRating" visible="false">
                        <label class="control-label col-md-2">
                            Select Rating
                        </label>
                        <div class="col-md-3">
                           <asp:Rating ID="RatingQ3" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star" AutoPostBack="true" OnChanged="RatingQ3_Changed"
                                        FilledStarCssClass="FilledStar">
                                    </asp:Rating>
                        </div>
                    </div>
                        <div class="col-md-12" runat="server"  id="divDoctor" visible="false">
                                <label class="control-label col-md-2">
                                    Select Doctor
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDoctor" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="ddlDoctor" ForeColor="Red" InitialValue="0">Please Select Doctor</asp:RequiredFieldValidator>
                                </div>
                            </div>

                     <div class="col-md-12" runat="server"  id="divLocation" visible="false">
                               <div class="col-md-12">
                             <label class="control-label col-md-2">
                                       Select State
                                    </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                       >
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlState" ForeColor="Red" InitialValue="0">Please Select State</asp:RequiredFieldValidator>
                                </div>
                                 </div>
                             <div class="col-md-12">
                             <label class="control-label col-md-2">
                                      Select District
                                    </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="ddlDistrict" ForeColor="Red" InitialValue="0">Please Select District</asp:RequiredFieldValidator>
                                </div>
                                 </div>

                          
                              <div class="col-md-12">
                             <label class="control-label col-md-2">
                                      Select Taluka
                                    </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlTaluka" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTaluka_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="ddlTaluka" ForeColor="Red" InitialValue="0">Please Select Taluka</asp:RequiredFieldValidator>
                                </div>
                                 </div>
                           <div class="col-md-12">
                             <label class="control-label col-md-2">
                                      Select City
                                    </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="ddlCity" ForeColor="Red" InitialValue="0">Please Select City</asp:RequiredFieldValidator>
                                </div>
                                 </div>
                            </div>

                    
                            <div class="col-md-12">
                                 <label class="control-label col-md-2">
                           
                        </label>
                                <div class="col-md-3">
                                         <asp:Button ID="brnReset" Text="Reset" runat="server" CssClass="btn btn-sm btn-danger"
                                             CausesValidation="false" OnClick="brnReset_Click" />
                                </div>
                            </div>

                    <div style="clear: both; height: 15px;">
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-vcenter table-condensed table-bordered dataTable grid-data"
                                PageSize="100"
                                CellPadding="9" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                <PagerStyle CssClass="pagination pagination-sm remove-margin" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sr. No." ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="HospitalName" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblHospitalName" runat="server" Text='<%#Bind("HospitalName")%>'></asp:Label>
                                            <asp:HiddenField ID="HfHospitalId" runat="server" Value='<%#Bind("HospitalId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblHospitalEmail" runat="server" Text='<%#Bind("HospitalEmail")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Phone Number" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblPhoneNumber" runat="server" Text='<%#Bind("PhoneNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="State" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblState" runat="server" Text='<%#Bind("State")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblDistrict" runat="server" Text='<%#Bind("District")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Taluka" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblTaluka" runat="server" Text='<%#Bind("Taluka")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="City/Area" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblCity" runat="server" Text='<%#Bind("City")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblAddress" runat="server" Text='<%#Bind("Address")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cost" ItemStyle-CssClass="gridText-left">
                                        <ItemTemplate>
                                            <asp:Label CssClass="me" ID="lblCost" runat="server" Text='<%#Bind("Cost")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Rating">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" AlternateText="Edit" CommandName="Updates"
                                                        CommandArgument='<%#Bind("HospitalId")%>' CausesValidation="false" data-toggle="tooltip"
                                                        ToolTip="View" class="btn btn-xs btn-default">
                                      Review And Rating
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                   
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

