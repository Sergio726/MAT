<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ServicioEdit.aspx.cs" Inherits="ServicioEdit" Title="Servicio Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Servicio - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="ServicioId" runat="server" DataSourceID="ServicioDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ServicioFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ServicioFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Servicio not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ServicioDataSource ID="ServicioDataSource" runat="server"
			SelectMethod="GetByServicioId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="ServicioId" QueryStringField="ServicioId" Type="String" />

			</Parameters>
		</data:ServicioDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPaqueteServicio1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaqueteServicio1_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteServicioDataSource1"
			DataKeyNames="PaqueteServicioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaqueteServicio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Servicio Id" DataNavigateUrlFormatString="ServicioEdit.aspx?ServicioId={0}" DataNavigateUrlFields="ServicioId" DataContainer="ServicioIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Servicio Found! </b>
				<asp:HyperLink runat="server" ID="hypPaqueteServicio" NavigateUrl="~/admin/PaqueteServicioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteServicioDataSource ID="PaqueteServicioDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteServicioProperty Name="Paquete"/> 
					<data:PaqueteServicioProperty Name="Servicio"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteServicioFilter  Column="ServicioId" QueryStringField="ServicioId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteServicioDataSource>		
		
		<br />
		

</asp:Content>

