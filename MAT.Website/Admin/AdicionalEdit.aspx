<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="AdicionalEdit.aspx.cs" Inherits="AdicionalEdit" Title="Adicional Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Adicional - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="AdicionalId" runat="server" DataSourceID="AdicionalDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/AdicionalFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/AdicionalFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Adicional not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:AdicionalDataSource ID="AdicionalDataSource" runat="server"
			SelectMethod="GetByAdicionalId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="AdicionalId" QueryStringField="AdicionalId" Type="String" />

			</Parameters>
		</data:AdicionalDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPasajeAdicional1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPasajeAdicional1_SelectedIndexChanged"			 			 
			DataSourceID="PasajeAdicionalDataSource1"
			DataKeyNames="PasajeAdicionalId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PasajeAdicional.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Pasaje Id" DataNavigateUrlFormatString="PasajeEdit.aspx?PasajeId={0}" DataNavigateUrlFields="PasajeId" DataContainer="PasajeIdSource" DataTextField="FechaReserva" />
				<data:HyperLinkField HeaderText="Adicional Id" DataNavigateUrlFormatString="AdicionalEdit.aspx?AdicionalId={0}" DataNavigateUrlFields="AdicionalId" DataContainer="AdicionalIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pasaje Adicional Found! </b>
				<asp:HyperLink runat="server" ID="hypPasajeAdicional" NavigateUrl="~/admin/PasajeAdicionalEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PasajeAdicionalDataSource ID="PasajeAdicionalDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeAdicionalProperty Name="Adicional"/> 
					<data:PasajeAdicionalProperty Name="Pasaje"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PasajeAdicionalFilter  Column="AdicionalId" QueryStringField="AdicionalId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PasajeAdicionalDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPaqueteAdicional2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaqueteAdicional2_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteAdicionalDataSource2"
			DataKeyNames="PaqueteAdicionalId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaqueteAdicional.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Adicional Id" DataNavigateUrlFormatString="AdicionalEdit.aspx?AdicionalId={0}" DataNavigateUrlFields="AdicionalId" DataContainer="AdicionalIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Adicional Found! </b>
				<asp:HyperLink runat="server" ID="hypPaqueteAdicional" NavigateUrl="~/admin/PaqueteAdicionalEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteAdicionalDataSource ID="PaqueteAdicionalDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteAdicionalProperty Name="Adicional"/> 
					<data:PaqueteAdicionalProperty Name="Paquete"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteAdicionalFilter  Column="AdicionalId" QueryStringField="AdicionalId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteAdicionalDataSource>		
		
		<br />
		

</asp:Content>

