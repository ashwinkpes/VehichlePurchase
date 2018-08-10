import { KeyValuePair } from './../../Models/vehichle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../../Models/vehichle';

@Component({
  selector: 'vehiche-list',
  templateUrl: './vehiche-list.component.html',
  styleUrls: ['./vehiche-list.component.css']
})
export class VehicheListComponent implements OnInit {
  private readonly PAGE_SIZE = 5; 
  queryResult: any = {};
  makes:KeyValuePair[];
  query:any = {PageSize: this.PAGE_SIZE}
  columns = [
    { title: 'Id' },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { }
  ];
  constructor(private vehicleService : VehicleService) { 
    
    this.makes = [];
  }

  ngOnInit() {
    this.getMakes();
    this.loadVehichles();
  }

  getMakes()
  {
    this.vehicleService.getMakes().subscribe( m => {
      this.makes = m;     
    });
  }

  loadVehichles()
  {
    this.populateVehichles();
  }



  onFilterChange()
  {
    this.query.Page = 1;    
    this.populateVehichles();
  }

  sortBy(columnName:string)
  {
     console.log('sortby called!!!');
    if (this.query.SortBy === columnName)
     {
      this.query.IsSortAscending = !this.query.IsSortAscending;
     }else{
      this.query.IsSortAscending = true;
      this.query.SortBy = columnName
     }

     this.populateVehichles();
  }

  private populateVehichles()
  {
    this.vehicleService.getAllVehichles(this.query).subscribe( v=> {
      this.queryResult  = v;
    })
  }

  onPageChange(page:any) {
    this.query.Page = page; 
    this.populateVehichles();
  }
}
