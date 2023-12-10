<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PlanillaServicioItemEdit.aspx.cs" Inherits="PlanillaServicioItemEdit" Title="PlanillaServicioItem Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Planilla Servicio Item - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PlanillaServicioItemId" runat="server" DataSourceID="PlanillaServicioItemDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaServicioItemFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaServicioItemFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PlanillaServicioItem not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PlanillaServicioItemDataSource ID="PlanillaServicioItemDataSource" runat="server"
			SelectMethod="GetByPlanillaServicioItemId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PlanillaServicioItemId" QueryStringField="PlanillaServicioItemId" Type="String" />

			</Parameters>
		</data:PlanillaServicioItemDataSource>
		
		<br />

		

</asp:Content>

