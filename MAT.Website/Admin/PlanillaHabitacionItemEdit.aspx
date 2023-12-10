<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PlanillaHabitacionItemEdit.aspx.cs" Inherits="PlanillaHabitacionItemEdit" Title="PlanillaHabitacionItem Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Planilla Habitacion Item - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PlanillaHabitacionItemId" runat="server" DataSourceID="PlanillaHabitacionItemDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaHabitacionItemFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaHabitacionItemFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PlanillaHabitacionItem not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PlanillaHabitacionItemDataSource ID="PlanillaHabitacionItemDataSource" runat="server"
			SelectMethod="GetByPlanillaHabitacionItemId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PlanillaHabitacionItemId" QueryStringField="PlanillaHabitacionItemId" Type="String" />

			</Parameters>
		</data:PlanillaHabitacionItemDataSource>
		
		<br />

		

</asp:Content>

