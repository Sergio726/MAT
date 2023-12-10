<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ReservaHabitacionEdit.aspx.cs" Inherits="ReservaHabitacionEdit" Title="ReservaHabitacion Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Reserva Habitacion - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="ReservaHabitacionId" runat="server" DataSourceID="ReservaHabitacionDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ReservaHabitacionFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ReservaHabitacionFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>ReservaHabitacion not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ReservaHabitacionDataSource ID="ReservaHabitacionDataSource" runat="server"
			SelectMethod="GetByReservaHabitacionId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="ReservaHabitacionId" QueryStringField="ReservaHabitacionId" Type="String" />

			</Parameters>
		</data:ReservaHabitacionDataSource>
		
		<br />

		

</asp:Content>

