"""Getting IMU Quaternions

"""
import numpy as np
import traceback
import serial_operations as serial_op
from colors import *
import UdpComms as U


# Create UDP socket to use for sending (and receiving)
sock = U.UdpComms(udpIP="127.0.0.1", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)
# Set parameters that will be configured
imu_configuration = {
    "disableCompass": True,
    "disableGyro": False,
    "disableAccelerometer": False,
    "gyroAutoCalib": True,
    "filterMode": 1,
    "tareSensor": True,
    "logical_ids": [1, 2, 3, 4, 5],
    "streaming_commands": [1, 255, 255, 255, 255, 255, 255, 255]
}
serial_port = serial_op.initialize_imu(imu_configuration)


while True:
    try:
        #Calibrate sensor from Unity
        data = sock.ReadReceivedData()
        if data != None:
            print(data)

            if data == "c":
                print("Received data from Unity ")

                data = None

        bytes_to_read = serial_port.inWaiting()

        # Read data from sensors and sent to Unity in better format.
        if 0 < bytes_to_read > 80:
            data = serial_port.read(bytes_to_read)
            if data[0] != 0:
                continue
            # euler_data = serial_op.extract_euler_angles(data)
            # str_euler_data = f"{euler_data[0]:.4f},{euler_data[1]:.4f},{euler_data[2]:.4f}"
            # print(f"IMU{data[1]:}" + str_euler_data)

            
            euler_data = serial_op.extract_quaternions(data)
            y_data = round(euler_data[1] * 180 / 3.14) 

            str_euler_data = f"{euler_data[0]:.4f},{euler_data[1]:.4f},{euler_data[2]:.4f}"
            
            print(f"IMU{data[1]}:" + str(y_data))

            sock.SendData(str(y_data))

    except KeyboardInterrupt:
        print(GREEN, "Keyboard excpetion occured.", RESET)
        serial_port = serial_op.stop_streaming(serial_port,
                                               imu_configuration['logical_ids'])
        break
    except Exception:
        print(RED, "Unexpected exception occured.", RESET)
        print(traceback.format_exc())
        print(GREEN, "Stop streaming.", RESET)
        serial_port = serial_op.stop_streaming(serial_port, 
                                               imu_configuration['logical_ids'])
        break

# def filter(q):
#     mf_window.pop(0)
#     mf_window.append(q)
#
#     x_list = [quat[0] for quat in mf_window]
#     y_list = [quat[1] for quat in mf_window]
#     z_list = [quat[2] for quat in mf_window]
#     w_list = [quat[3] for quat in mf_window]
#
#     x_list.sort()
#     y_list.sort()
#     z_list.sort()
#     w_list.sort()
#
#     qx = x_list[len(x_list) // 2]
#     qy = y_list[len(x_list) // 2]
#     qz = z_list[len(x_list) // 2]
#     qw = w_list[len(x_list) // 2]
#
#     return [qx, qy, qz, qw]
