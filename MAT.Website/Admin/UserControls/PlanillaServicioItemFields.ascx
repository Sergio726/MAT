<%@ Control Language="C#" ClassName="PlanillaServicioItemFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPlanillaServicioItemId" runat="server" Text="Planilla Servicio Item Id:" AssociatedControlID="dataPlanillaServicioItemId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPlanillaServicioItemId" Value='<%# Bind("PlanillaServicioItemId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPlanillaId" runat="server" Text="Planilla Id:" AssociatedControlID="dataPlanillaId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPlanillaId" DataSourceID="PlanillaIdPlanillaDataSource" DataTextField="ViajeId" DataValueField="PlanillaId" SelectedValue='<%# Bind("PlanillaId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:PlanillaDataSource ID="PlanillaIdPlanillaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataServicioId" runat="server" Text="Servicio Id:" AssociatedControlID="dataServicioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataServicioId" Value='<%# Bind("ServicioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCantidad" runat="server" Text="Cantidad:" AssociatedControlID="dataCantidad" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCantidad" Text='<%# Bind("Cantidad") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataCantidad" runat="server" Display="Dynamic" ControlToValidate="dataCantidad" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataCantidad" runat="server" Display="Dynamic" ControlToValidate="dataCantidad" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataSubtotal" runat="server" Text="Subtotal:" AssociatedControlID="dataSubtotal" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataSubtotal" Text='<%# Bind("Subtotal") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataSubtotal" runat="server" Display="Dynamic" ControlToValidate="dataSubtotal" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataSubtotal" runat="server" Display="Dynamic" ControlToValidate="dataSubtotal" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


