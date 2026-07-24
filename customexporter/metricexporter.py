import logging
import argparse
import os
import time
from glob import glob
from prometheus_client import start_http_server, Gauge

# Logging 
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(name)s - %(levelname)s - %(message)s')

# Setting the arguments
parser = argparse.ArgumentParser()

# required arg
parser.add_argument('-f', '--folder', required=True, help="target folder")
parser.add_argument('-e', '--extension', required=True, help="extension to check")
args = parser.parse_args()

class CustomExporter:
    def __init__(self, folder:str, ext:str) -> None:
        self.folder = folder
        self.ext = ext
        self.metric_dict = {}

    def count_file_in_folder(self) -> int:
        '''
        DOCSTRING: Count the number of files with a specific extension 
        INPUT: folder:str, ext:str
        OUTPUT: number (int)
        '''
        file_list = [f for f in glob(f"{self.folder}/*.{self.ext}")]
        logging.info(f'Founds {len(file_list)} file(s) in folder {self.folder} with extension {self.ext}')
        return len(file_list)

    def create_gauge_for_metric(self, metric_name):
        if self.metric_dict.get(metric_name) is None:
            self.metric_dict[metric_name] = Gauge(metric_name, f"number of *{self.ext} files in {self.folder}")
    
    def set_value(self, metric_name):
        self.metric_dict[metric_name].set(self.count_file_in_folder())

    def main(self):
        exporter_port = int(os.environ.get("EXPORTER_PORT", "9877"))
        start_http_server(exporter_port)
        metric_name = f"cust_{self.ext}_files_in_{self.folder.replace('/', '_')}_total"
        while True:
            self.create_gauge_for_metric(metric_name)
            self.set_value(metric_name)
            time.sleep(10)

if __name__ == "__main__":
    folder, extension = args.folder, args.extension
    c = CustomExporter(folder, extension)
    c.main()