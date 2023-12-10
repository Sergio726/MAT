<%@ Control Language="C#" ClassName="PasajeroFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeroId" runat="server" Text="Pasajero Id:" AssociatedControlID="dataPasajeroId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPasajeroId" DataSourceID="PasajeroIdPersonaDataSource" DataTextField="Apellido" DataValueField="PersonaId" SelectedValue='<%# Bind("PasajeroId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:PersonaDataSource ID="PasajeroIdPersonaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasaporte" runat="server" Text="Pasaporte:" AssociatedControlID="dataPasaporte" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPasaporte" Text='<%# Bind("Pasaporte") %>' MaxLength="100"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVencimientoPasaporte" runat="server" Text="Vencimiento Pasaporte:" AssociatedControlID="dataVencimientoPasaporte" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataVencimientoPasaporte" Text='<%# Bind("VencimientoPasaporte", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataVencimientoPasaporte" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEmisionPasaporte" runat="server" Text="Emision Pasaporte:" AssociatedControlID="dataEmisionPasaporte" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEmisionPasaporte" Text='<%# Bind("EmisionPasaporte", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataEmisionPasaporte" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaisOrigen" runat="server" Text="Pais Origen:" AssociatedControlID="dataPaisOrigen" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPaisOrigen" Text='<%# Bind("PaisOrigen") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


