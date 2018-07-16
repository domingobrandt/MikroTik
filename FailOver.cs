//ip route
add distance=1 gateway=190.73.32.1 routing-mark=to-wan1
add distance=2 gateway=190.73.32.1 routing-mark=to-wan2
 //ahora va el fail over lo que hace que cuando una se cae la otra trabaje por la dos
 //ip firewall mangle

add action=mark-connection chain=prerouting connection-mark=no-mark in-interface=AQUI EL NOMBRE DE TU WAN1 new-connection-mark=ISP1_conn passthrough=yes
add action=mark-connection chain=prerouting connection-mark=no-mark in-interface=AQUI EL NOMBRE DE TU WAN2 new-connection-mark=ISP2_conn passthrough=yes

add action=mark-connection chain=prerouting connection-mark=no-mark dst-address-type=!local in-interface=AQUI EL NOMBRE DE TU LAN new-connection-mark=ISP1_conn passthrough=yes per-connection-classifier=both-addresses:2/0
add action=mark-connection chain=prerouting connection-mark=no-mark dst-address-type=!local in-interface=AQUI EL NOMBRE DE TU LAN new-connection-mark=ISP2_conn passthrough=yes per-connection-classifier=both-addresses:2/1

add action=mark-routing chain=prerouting connection-mark=ISP1_conn in-interface=ether1-Lan new-routing-mark=to-wan1 passthrough=no
add action=mark-routing chain=prerouting connection-mark=ISP2_conn in-interface=ether1-Lan new-routing-mark=to-wan2 passthrough=no

add action=mark-routing chain=output connection-mark=ISP1_conn new-routing-mark=to-wan1 passthrough=no
add action=mark-routing chain=output connection-mark=ISP2_conn new-routing-mark=to-wan2 passthrough=no