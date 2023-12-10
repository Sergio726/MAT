<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PlanillaServicioEdit.aspx.cs" Inherits="PlanillaServicioEdit" Title="PlanillaServicio Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Planilla Servicio - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PlanillaServicioId" runat="server" DataSourceID="PlanillaServicioDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaServicioFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaServicioFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>PlanillaServicio not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<%--<data:PlanillaServicioDataSource ID="PlanillaServicioDataSource" runat="server"
			SelectMethod="GetByPlanillaServicioId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PlanillaServicioId" QueryStringField="PlanillaServicioId" Type="String" />--%>

			<%--</Parameters>
		</data:PlanillaServicioDataSource>
		--%>
		<br />

		<data:EntityGridView ID="GridViewPlanillaServicioItem1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPlanillaServicioItem1_SelectedIndexChanged"			 			 
			DataSourceID="PlanillaServicioItemDataSource1"
			DataKeyNames="PlanillaServicioItemId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PlanillaServicioItem.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Planilla Servicio Id" DataNavigateUrlFormatString="PlanillaServicioEdit.aspx?PlanillaServicioId={0}" DataNavigateUrlFields="PlanillaServicioId" DataContainer="PlanillaServicioIdSource" DataTextField="ViajeId" />
				<asp:BoundField DataField="ServicioId" HeaderText="Servicio Id" SortExpression="[ServicioID]" />				
				<asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="[Cantidad]" />				
				<asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="[Subtotal]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Planilla Servicio Item Found! </b>
				<asp:HyperLink runat="server" ID="hypPlanillaServicioItem" NavigateUrl="~/admin/PlanillaServicioItemEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PlanillaServicioItemDataSource ID="PlanillaServicioItemDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<%--<data:PlanillaServicioItemProperty Name="PlanillaServicio"/>--%> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<%--<data:PlanillaServicioItemFilter  Column="PlanillaServicioId" QueryStringField="PlanillaServicioId" />--%> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PlanillaServicioItemDataSource>		
		
		<br />
		

</asp:Content>

