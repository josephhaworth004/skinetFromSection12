import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

// onInitialization is another lifecycle hook
// It's provided as an interface
export class AppComponent implements OnInit{
  title = 'Skinet';

// Lifecycle hooks. Various stages an entity goes through
// Inject something into constructor
// Make it private so we can only use http in this class

  constructor() {}

  // Normally considered too early to call this inside constructor
  // Typically use constructor for dependency injection
  ngOnInit(): void {
    // Will return an observable of the response body as a js object
    // Must subscribe to observable
    // will auto-unsubscribe when complete

  }
}

