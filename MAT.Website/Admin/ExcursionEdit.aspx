<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ExcursionEdit.aspx.cs" Inherits="ExcursionEdit" Title="Excursion Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Excursion - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="ExcursionId" runat="server" DataSourceID="ExcursionDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ExcursionFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ExcursionFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Excursion not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ExcursionDataSource ID="ExcursionDataSource" runat="server"
			SelectMethod="GetByExcursionId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="ExcursionId" QueryStringField="ExcursionId" Type="String" />

			</Parameters>
		</data:ExcursionDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPaqueteExcursion1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaqueteExcursion1_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteExcursionDataSource1"
			DataKeyNames="PaqueteExcursionId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaqueteExcursion.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Excursion Id" DataNavigateUrlFormatString="ExcursionEdit.aspx?ExcursionId={0}" DataNavigateUrlFields="ExcursionId" DataContainer="ExcursionIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Excursion Found! </b>
				<asp:HyperLink runat="server" ID="hypPaqueteExcursion" NavigateUrl="~/admin/PaqueteExcursionEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteExcursionDataSource ID="PaqueteExcursionDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteExcursionProperty Name="Excursion"/> 
					<data:PaqueteExcursionProperty Name="Paquete"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteExcursionFilter  Column="ExcursionId" QueryStringField="ExcursionId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteExcursionDataSource>		
		
		<br />
		

</asp:Content>

