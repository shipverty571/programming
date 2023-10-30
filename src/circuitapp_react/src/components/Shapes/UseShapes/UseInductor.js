import React, {Component} from 'react';

class UseInductor extends Component {
    render() {
        return (
            <use 
                x={this.props.x} 
                y={this.props.y} 
                href={this.props.href} 
                className="draggable" 
                style={{cursor: this.props.canNotDraggable && "default"}} />
        );
    }
}

export default UseInductor;