import { ProgressService } from './../../services/progress.service';
import { PhotoService } from './../../services/photo.service';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  vehicle:any;
  vehicleId :number = 0;
  @ViewChild('fileInput') fileInput: ElementRef;
  photos:any[] = [];
  percentage:any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    private progressService: ProgressService) {
     
      route.params.subscribe(p => {
        this.vehicleId = +p['id'];       
        if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
          router.navigate(['/vehicles']);
          return; 
        }
      })
   }

  ngOnInit() {
    if (this.vehicleId) {
      this.populateVehicle();
    }  

    this.getPhotos();
  }

  private getPhotos()
  {
    this.photoService.getPhotos(this.vehicleId).subscribe(ph => {this.photos = ph})
  }

  private populateVehicle()
  {    
    this.vehicleService.getVehichle(this.vehicleId).subscribe( v=> {
       this.vehicle = v;
     },
      err => {
        if (err.status === 404)
        {
          this.router.navigate(['/vehicles']);
          return; 
        }
      }
    )
  }

  uploadPhoto()
  {
   

    this.progressService.startTracking()
    .subscribe(progress => {
      this.percentage = progress.percentage
    },err => {
      console.log('error occured during file progress recording !!!');
    },  () => {this.percentage = null;});

    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    if (nativeElement.files)
    {
      var file = nativeElement.files[0];
      nativeElement.value = '';
      this.photoService.uploadPhoto(this.vehicleId, file)
      .subscribe(x =>{this.photos.push(x)});
    }
   
  }

}
