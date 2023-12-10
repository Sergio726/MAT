<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ViajeHotelEdit.aspx.cs" Inherits="ViajeHotelEdit" Title="ViajeHotel Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Viaje Hotel - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="ViajeHotelId" runat="server" DataSourceID="ViajeHotelDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ViajeHotelFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ViajeHotelFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>ViajeHotel not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ViajeHotelDataSource ID="ViajeHotelDataSource" runat="server"
			SelectMethod="GetByViajeHotelId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="ViajeHotelId" QueryStringField="ViajeHotelId" Type="String" />

			</Parameters>
		</data:ViajeHotelDataSource>
		
		<br />

		

</asp:Content>

